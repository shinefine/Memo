const Trigger = require('./Triggers');
const R  = require('ramda');
let  typesDef  = require("./CommonTypeDef");
var  util = require('./util'); //要使用extend 方法
var Memory = require('./MemoryLib');

let Signal = util.Signal;
let messageType = typesDef.MessageType;
let triggerTags = typesDef.TriggerTags;
let dataTags    = typesDef.DataTags;
let signalType  = typesDef.SignalType;
let modelType   = typesDef.ConceptModelType;
var triggerList = []

//词语敏感触发器

tig_word_sensitive = new Trigger("Tig词聚合处理",
    [        
        triggerTags.TypeIsGrammarPrase
    ],

    (signal)=>{return signal.SignalType == signalType.CharInSentence},  

    //处理方式
    R.curry(

        (context,signal)=>{    
        // (signal)=>{    
        [result_1,result_2,result_3]=[[],[],[]];

        //对于一个单一字符，首先      尝试看看它有没有单字表达的意思
        result_1 = Memory.querySingleCharWord(signal.data).map(w=> {
            
                                                            let sign =  new Signal(
                                                                {
                                                                    wordText: w,
                                                                    wordCharIndex:[signal.charIndex]
                                                                }
                                                                ,signal.id
                                                                ,signalType.SingleCharWord
                                                            )
                                                            sign.taskSignalId = signal.taskSignalId
                                                            return sign;
                                                        });

        //如果该字符不是句子里的首字，则看它和它的前置字符是否能够聚合成为一个词 /(或是一个词的词首)
        if(signal.charIndex > 0 ){
            
            //查找前一个字符,按照charIndex 属性查找  //【bug已修正】: 注意这里如果仅以charIndex 做筛选条件的，则有可能 两句/多独立的话的字符相互混淆干扰查询
            let prevSignalList = Memory.querySignalBy((s)=>s.SignalType==signalType.CharInSentence
                                                        && s.taskSignalId == signal.taskSignalId
                                                        && s.charIndex == signal.charIndex - 1 
                                                        );
            //assert prevSignals.length == 1;
            let prevSignal = prevSignalList[0];
            //assert(prevSignal != null )
            let word = prevSignal.data + signal.data;
            
              //【优化代码，如果当前字符已是句尾字符，则不必查询预测词（result_3），这种情况不用调Memory.queryWordStartWith()方法，可以直接调queryWord()】
            let exceptWordList = Memory.queryWordStartWith(word); //有可能查询出一个词,也有可能查不到这个词

            //【已优化代码，if逻辑 改成 filter，之后这里可以变成流】
          
            result_2 = exceptWordList.filter(w=>w.length == word.length)//这是一个确定的词语
                                        .map(w=> {
            
                                                let sign =  new Signal(
                                                    {
                                                        wordText: word,
                                                        wordCharIndex:[prevSignal.charIndex,signal.charIndex]
                                                    }
                                                    ,signal.id
                                                    ,signalType.WordAggregation
                                                )
                                                sign.taskSignalId = signal.taskSignalId
                                                return sign;
                                            });
                                    
            result_3 = exceptWordList.filter(w=>w.length > word.length)//这是一个期待的词语
                                        .map(w=> {
            
                                            let expectStr = w.substr(word.length);

                                            let sign = new Signal(
                                                {
                                                    wordText:word,
                                                    wordCharIndex:[prevSignal.charIndex,signal.charIndex],
                                                    expectText:expectStr,
                                                    expectCharIndex:R.range(signal.charIndex + 1,signal.charIndex + 1 + expectStr.length)
                            
                                                }
                                                ,signal.id
                                                ,signalType.WordExpect
                                            )
                                            sign.taskSignalId = signal.taskSignalId
                                            return sign;
                                            });

                                            
        
        
        }
        //注意这里的逻辑，如果当前字符是句尾字符，则不发出关于预测词信号，
        //如果修改这里的逻辑，会影响到预测词触发器的 效果，即是说这里的逻辑与预测词触发器的行为逻辑相互纠缠，修改这里的逻辑时需要修改另一边的逻辑代码
        if(signal.EOFChar)
        {
            sentenceEndSign = new Signal(                
                "表示句子结束的信号不带有特定意义的数据"                
                ,signal.id
                ,signalType.SentenceIsEnd
            );
            sentenceEndSign.taskSignalId = signal.taskSignalId;
            return  result_1.concat(result_2).concat([sentenceEndSign]);  //返回值是 [signal,signal,signal....]
        }else{
            return  result_1.concat(result_2,result_3);  //返回值是 [signal,signal,signal....]
        }
        
    }
    )
);





//预测期望词触发器

tig_word_except= new Trigger("Tig词预测处理",
    [],
    (signal)=>{return signal.SignalType == signalType.WordExpect},  
    R.curry(
        (context,signal)=>{
            
            let masterStream = context.masterStream;
            //let memory = context.memory;


            console.log("词预测处理器尝试匹配："+ signal.data.wordText+signal.data.expectText);
            let cur_subscribe = masterStream.filter(sign=>sign.SignalType ==  signalType.CharInSentence
                                                    && sign.taskSignalId == signal.taskSignalId
            ) //这里少了一个对taskId的匹配条件
            .map(sign=>sign.data)
            .bufferCount(signal.data.expectCharIndex.length)            
            .subscribe(charArr=>{
                let succStr = charArr.join('');
                console.log("后续字符:"+succStr + " 被["+ signal.data.wordText+"->"+signal.data.expectText +"]处理");
                let signalArr =[];
                if(succStr == signal.data.expectText){
                    let sign =  new Signal(
                        {
                            wordText:signal.data.wordText + signal.data.expectText,
                            wordCharIndex:signal.data.wordCharIndex.concat(signal.data.expectCharIndex)
                        }
                        ,signal.id
                        ,signalType.WordAggregation
                    )
                    sign.taskSignalId = signal.taskSignalId
                    
                    let sign_end =new Signal("预测词触发器的成功匹配："+signal.data.wordText + "->" +signal.data.expectText,signal.id,signalType.TestTempType)
                    signalArr = [sign,sign_end]
                }else{       
                    
                    signalArr = [new Signal("预测词触发器的失败匹配：" +signal.data.wordText +"->"+signal.data.expectText ,signal.id,signalType.TestTempType)];
                  
                }

                /*对于subject 对象来说，不能在map()方法里传递next()方法，会报错:
                TypeError: Cannot read property 'closed' of undefined
                    at Subject.next (C:\Users\shine\AppData\Roaming\npm\node_modules\rxjs\Subject.js:47:18)
                    at Array.map (<anonymous>)
                需要在map方法里显式调用next方法
                */
                //signalArr.map(context_stream.next);
                cur_subscribe.unsubscribe();

                signalArr.map(sig=>masterStream.next(sig));

            });

            return [new Signal("来自预测词触发器的信号",signal.id,signalType.TestTempType)];
        }
    )
);








//当一句话结束时，（聚合词，单字词都已处理完毕）触发器
//分析这些词语表示的意思，组装好一个概念模型，交给下一个触发器处理

tig_sentence_end= new Trigger("Tig句子结束处理",
    [],
    (signal)=>{return signal.SignalType == signalType.SentenceIsEnd},  
    R.curry(
        (context,signal)=>{
            let masterStream = context.masterStream;
            let memory = context.memory;

            var wordList = memory.querySignals(sig=>(sig.SignalType == signalType.WordAggregation || sig.SignalType == signalType.SingleCharWord)
            && sig.taskSignalId == signal.taskSignalId)    
            //当此触发器被激活时，相关数据已经准备好了（句子里所有可能的聚合词和单字词）        
            //console.log(wordList);            


            

            /*
            [ Signal {
    id: 10,
    timestamp: 1521168907447,
    data: { wordText: '果', wordCharIndex: [Array] },
    sourceSignalId: 4,
    SignalType: '这是一个单字词语',
    taskSignalId: 2 },
  Signal {
    id: 11,
    timestamp: 1521168907447,
    data: { wordText: '苹果', wordCharIndex: [Array] },
    sourceSignalId: 4,
    SignalType: '聚合成了词语',
    taskSignalId: 2 },
  Signal {
    id: 14,
    timestamp: 1521168907448,
    data: { wordText: '是', wordCharIndex: [Array] },
    sourceSignalId: 5,
    SignalType: '这是一个单字词语',
    taskSignalId: 2 },
  Signal {
    id: 16,
    timestamp: 1521168907449,
    data: { wordText: '不', wordCharIndex: [Array] },
    sourceSignalId: 6,
    SignalType: '这是一个单字词语',
    taskSignalId: 2 },
  Signal {
    id: 17,
    timestamp: 1521168907449,
    data: { wordText: '是不', wordCharIndex: [Array] },
    sourceSignalId: 6,
    SignalType: '聚合成了词语',
    taskSignalId: 2 },
  Signal {
    id: 20,
    timestamp: 1521168907450,
    data: { wordText: '是', wordCharIndex: [Array] },
    sourceSignalId: 7,
    SignalType: '这是一个单字词语',
    taskSignalId: 2 },
  Signal {
    id: 21,
    timestamp: 1521168907450,
    data: { wordText: '不是', wordCharIndex: [Array] },
    sourceSignalId: 7,
    SignalType: '聚合成了词语',
    taskSignalId: 2 },
  Signal {
    id: 22,
    timestamp: 1521168907450,
    data: { wordText: '是不是', wordCharIndex: [Array] },
    sourceSignalId: 18,
    SignalType: '聚合成了词语',
    taskSignalId: 2 },
  Signal {
    id: 24,
    timestamp: 1521168914182,
    data: { wordText: '水', wordCharIndex: [Array] },
    sourceSignalId: 8,
    SignalType: '这是一个单字词语',
    taskSignalId: 2 },
  Signal {
    id: 25,
    timestamp: 1521168914182,
    data: { wordText: '果', wordCharIndex: [Array] },
    sourceSignalId: 9,
    SignalType: '这是一个单字词语',
    taskSignalId: 2 },
  Signal {
    id: 26,
    timestamp: 1521168914182,
    data: { wordText: '水果', wordCharIndex: [Array] },
    sourceSignalId: 9,
    SignalType: '聚合成了词语',
    taskSignalId: 2 } ]


             */

/*
    ---[果]
    --------[苹果]
    ---[是]
    ---[不]
    --------[是不]
    ---[是]
    --------[不是]
    ---------------[是不是]
    ---[水]
    ---[果]
    --------[水果]
    ---


 */

        sortedArr = wordList.sort((a,b)=>b.data.wordCharIndex.length - a.data.wordCharIndex.length)
        let usedSignals = _getMaxLengthWord(sortedArr).sort((a,b)=>a.data.wordCharIndex[0] - b.data.wordCharIndex[0]);
    
        //usedSignals  =[signal{"苹果"},signal{"是不是"},signal{"水果"}]

        //从记忆库中查询这些聚合词对应着哪些概念,将会引发哪些后续行为

        /*
        about("苹果")  苹果是一种范畴概念
        about("是不是")   疑问表达   真假值判断-->两个主体间存在某种关系的
        about("水果")--> 水果 是一种范畴概念

        */        
        //根据usedSignals里面的词,建立起一个表示意思的模型

        let wordSeq = usedSignals.map(s=>s.data.wordText)
        .filter(s=> s !="呢"); //特殊处理的代码，后续去掉 针对输入（-2）的情况


        let modelSignalList = ["是不是","是什么类型"]
                    .filter(word=> wordSeq.includes(word))
                    .map(word=>
                            {return  {
                                        description: "这是一个概念模型，表示疑问(想要确定一个事实称述的真假值)",
                                        modelType:modelType.Question,
                                        keyWord:word,
                                        keyWordIndex:1,
                                        conceptSeq: wordSeq//["苹果","是不是","水果"] //概念序列    
                                    };
                            }
                    ).map(model=> new Signal(model ,signal.id,signalType.ConceptModel)     );

        
        



        //查询记忆库,找到了 苹果 这个概念的相关信息
        concept1={
            wordText:"苹果",
            featuresTags:["__something__","__category__"],
            linkFeatures:["食用性","味道","重量","形状","颜色","体积"]
        }

        concept2={
            wordText:"水果",
            featuresTags:["__something__","__category__"],
            linkFeatures:["食用性","味道","重量","形状","颜色","体积"]
        }

        //在流中放置新信号

        //result_signal = new Signal(model ,signal.id,signalType.ConceptModel)       

        return modelSignalList.concat([new Signal("来自句子结束处理触发器的信号",signal.id,signalType.TestTempType)]);
        
    }
    )
);




//工具函数
 function _getMaxLengthWord(wordList)
 {
  /*简略实现的逻辑 */
    //找出最长的聚合词

   if(wordList.length ==0)
   {
       return [];
   }

   //递归调用
    return []
    .concat(wordList[0])  ////选出最长的聚合词 ,当前集合(已按照长度从大到小排序)的第一个元素
    .concat( _getMaxLengthWord(wordList.filter(s=>R.intersection(s.data.wordCharIndex,wordList[0].data.wordCharIndex).length == 0))); //将最长的聚合词遮蔽的字词从数组中去掉,然后将数组再次传入本方法以获取下一个最长的聚合词
    
 }




 var  tig_questionConcept1= new Trigger("问题模型处理",
    [],
    (signal)=>{return signal.SignalType == signalType.ConceptModel
         && signal.data.modelType == modelType.Question
         && signal.data.keyWord =="是不是"
        },  
    R.curry(
        (context,signal)=>{
            let data =signal.data;
            //return util.extend(true,{},data, {memoryQueryResult: Memory.queryMasterType(data.master,data.slave)}


            //var result = data.memoryQueryResult;

            var result =  Memory.queryMasterType(data.conceptSeq[0],data.conceptSeq[2]);
            var bool_flag = false;    
            if(result.length == 0){ //未查询到结果,常识推导功能介入解答问题
        
                // var res = CommonKnow.derivedCommonKnowledge_category(convert(str_master),convert(str_slave));
                // if(res){
                //     bool_flag = true;
                    
                // }
            }else{ //查询到了结果,
                bool_flag = true;
            }
            //回答
            if(bool_flag){
                  console.log(`${data.conceptSeq[0]}是一种${data.conceptSeq[2]}`) ;
            }else{
                console.log(`${data.conceptSeq[0]}不是一种${data.conceptSeq[2]}`)
            }   
        
         return [];   
        })
);

var  tig_questionConcept2= new Trigger("问题模型处理",
[],
(signal)=>{return signal.SignalType == signalType.ConceptModel
     && signal.data.modelType == modelType.Question
     && signal.data.keyWord =="是什么类型"
    },   
R.curry(
    (context,signal)=>{
        let data =signal.data;
        //return util.extend(true,{},data, {memoryQueryResult: Memory.queryMasterType(data.master,data.slave)}


        //var result = data.memoryQueryResult;

        var result =  Memory.queryMasterType(data.conceptSeq[0],data.conceptSeq[2]);
       
        if(result.length == 0){
            console.log(`我不知道 '${data.conceptSeq[0]}'是什么类型,我应该还没有学习这个知识`);
        }else{
            var msg = `${data.conceptSeq[0]}是${result.shift()[1]}类型`
            if(result.length>0){
                msg += `,它同时也是${result.map(i=>i[1]).join(",")}`;
            }
            console.log(msg)
        }   
        
    
        return [];   
    })
);


//==============================


module.exports={

    triggerList:[],
    specialTigs:[tig_word_sensitive,tig_word_except,tig_sentence_end,tig_questionConcept1,tig_questionConcept2],
   
};