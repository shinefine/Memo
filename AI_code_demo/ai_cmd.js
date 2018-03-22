//todo 接下来要解决的问题是,输入语言的模糊性 兼容功能,
/*
即,当我们输入 '苹果是什么类型'  , ai 能够回答 '苹果是一种水果'
而当我们输入 '苹果是什么' ,'苹果的类型?' 这种句式时,ai 也能理解并且做出相同的回答

同样,当输入 '苹果是不是水果' ,ai 能够回答 '是'
那么输入 '苹果是一种水果吗?' "苹果是水果吗?" ai 同样能够做出回答
**/









/*
*************模块的作用****************************
本js文件提供了 在命令行环境下的ai对话功能

通过在命令行下 运行 node ai_cmd 命令





*************本模块提供的接口:**********************


*********Code Example 如何使用本模块：**************

3. 外部代码使用本模块定义的方法
 

*/

const R  = require('ramda');

const readline = require('readline');

const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout,
  prompt: 'OHAI> '
});

let dict_cmd_flag = {
    Study_Word :"动作：添加主语文本到词库中",
    Study_TypeOfMaster:"动作：学习主体概念的类型特性",
    Answer_AnyTypeOfMaster:"动作：解答主体概念所属的类型",
    AnswerBoolean_SpecialTypeOfMaster:"动作：解答主体是否是某个特定的类型,回答的答案是真/假值",
    unknownCmd:"无法确定应该执行的动作",

};

//句子里包含的关键词应该触发的行为
let dict_keyWord_2_cmdFlag = [
    ["是不是",dict_cmd_flag.AnswerBoolean_SpecialTypeOfMaster]
    ,["是一种",dict_cmd_flag.Study_TypeOfMaster]
    ,["是一种词语",dict_cmd_flag.Study_Word]
    ,["是什么类型",dict_cmd_flag.Answer_AnyTypeOfMaster]                        
]

let shortCutCmd={
    "-1":"苹果是不是水果",
    "-2":"苹果是什么类型呢",
    "-3":"苹果是不是食物",
    "-4":"水果是不是食物",
}



var util  = require('./util');
let Signal = util.Signal;
let  typesDef  = require("./CommonTypeDef");
let  SignalType  = typesDef.SignalType;
console.log("AI正在启动...");
//加载记忆库
//load memory.lib
console.log("...加载记忆库...");
var Memory = require('./MemoryLib');
console.log("...加载常识功能库...");
var CommonKnow = require('./CommonKnowledge');

console.log("...加载公告板系统"); 
var intentionBoard = require('./RX_IntentionBoard')    ;

console.log("...加载触发器...");
let {triggerList ,specialTigs,specialTig } =  require('./TriggerLib');

console.log("...注册触发器...");

// triggerList.map(tig=>xxx(tig));
// triggerList.map(xxx);
//triggerList.forEach(tig=>intentionBoard.AddTrigger(tig));
//triggerList.map(tig=>intentionBoard.AddTrigger(tig));
//triggerList.map(intentionBoard.AddTrigger); //使用这种map风格写法时，注意，传递的方法 AddTrigger方法内部的this 变量绑定到了global对象上，而使用上句写法就没有问题
//intentionBoard.AddTrigger_WordAggregation(specialTig);
specialTigs.map(intentionBoard.AddTrigger_WordAggregation);//使用这种map风格写法时，注意，传递的方法 AddTrigger方法内部的this 变量绑定到了global对象上，而使用上句写法就没有问题
console.log("...导入预学习知识...");

["苹果是一种水果"
,"水果是一种食物"
,"苹果是一种食物"
,"红富士是一种苹果"].forEach(msg => inputSenetnceAndOutputResult(msg));

console.log("==========启动完成===========");

["输入'xxx是一种词语'让AI学习 词语 xxx"
,"输入'xxx是一种yyy' 让AI学习 xxx 属于种类 yyy"
,"输入'xxx是什么类型' 让AI回答一个答案"
,"输入'xxx是不是yyy'让AI回答一个肯定或否定的答案(依据所属的种类)"
 ].forEach(msg => console.log(msg));


rl.prompt();

rl.on('line', (sentence) => {
    inputSenetnceAndOutputResult(sentence);

   
  rl.prompt();

}).on('close', () => {
  console.log('Have a great day!');
  process.exit(0);
});

function xxx(abc){
    console.log("fgfffff");
}



function inputSenetnceAndOutputResult(sentence){
    let str = sentence.trim();   

    //如果输入的是快捷命令，则将其转换为对应的句子
    str = shortcut(str);

    //根据句子中的关键词判断应该执行什么操作，设置cmd_flag的值   
    let cmd_flag = parseSentenceToSetFlag(str);
   
    //根据cmd_flag 的值执行具体的操作
    doActionBySentenceMean(cmd_flag,str);
    
}

function shortcut(str){
    if(str[0] =="-"){
        console.log(shortCutCmd[str]);
        return shortCutCmd[str];
    }else{
        return str;
    }


}

//【应该替换掉此方法】这种预先在句子里找关键词 触发对应模式的 方式，是大多数对话机器人的做法，这里只是权益之计，之后应该替换掉
function parseSentenceToSetFlag(sentence){
    
    //这种预先在句子里找关键词 触发对应模式的 方式，是大多数对话机器人的做法，这里只是权益之计，之后应该替换掉
    var searchResult = dict_keyWord_2_cmdFlag.find(arr=>sentence.search(arr[0])>0)
    if(searchResult){
        return searchResult[1];
    }
    
    return dict_cmd_flag.unknownCmd;   
}

function doActionBySentenceMean(cmd_flag, sentence){
    let taskSig = new Signal("不带有具体数据,主要是后续要一个同义的taskID",0);

    switch (cmd_flag) {
        case  dict_cmd_flag.unknownCmd:
            console.log('---抱歉，我无法理解你输入的这句话的意思---');
            break;
        case dict_cmd_flag.Study_Word:
            //获得主语名称
            var str_master = sentence.replace("是一种词语","");

            //将词语str_master存入记忆库
            Memory.addWord(str_master);

            console.log(`我学会了新的词语 '${str_master}'`)
            break;
        case dict_cmd_flag.Study_TypeOfMaster:
            //获得主体名称和客体名称
            var arr = sentence.split("是一种")
            var str_master = arr[0];
            var str_slave= arr[1];

            Memory.studyType(str_master,str_slave);
            console.log(`我学会了 '${str_master}'的类型是  '${str_slave}'`);
            break;


        case dict_cmd_flag.Answer_AnyTypeOfMaster:

            var sig  = new Signal(sentence);
            intentionBoard.AddIntention(sig);


     //新方法走另外一条路径，将句子拆解成一个个字符然后顺序输入流中

        
            taskSig.SignalType = SignalType.Task_SentenceSplitWord;
            intentionBoard.AddIntention(taskSig);

            signalArr = sentence.split('')
            .map((c,index,arr)=>{
                let sig = new Signal(c,taskSig.id); //

                sig.taskSignalId=taskSig.id;//设置任务信号ID

                sig.charIndex = index;
                sig.SignalType = SignalType.CharInSentence;
                
                sig.EOFChar = (index == arr.length - 1);
                return sig;
            }).map(intentionBoard.AddIntention);
            

            break;
        case dict_cmd_flag.AnswerBoolean_SpecialTypeOfMaster:

            
            var sig  = new Signal(sentence);
            intentionBoard.AddIntention(sig);

            //新方法走另外一条路径，将句子拆解成一个个字符然后顺序输入流中

      
            taskSig.SignalType = SignalType.Task_SentenceSplitWord;
            intentionBoard.AddIntention(taskSig);

            signalArr = sentence.split('')
            .map((c,index,arr)=>{
                let sig = new Signal(c,taskSig.id); //

                sig.taskSignalId=taskSig.id;//设置任务信号ID

                sig.charIndex = index;
                sig.SignalType = SignalType.CharInSentence;
                
                sig.EOFChar = (index == arr.length - 1);
                return sig;
            }).map(intentionBoard.AddIntention);
            

            // R.reduce((prevId,signal)=>{
            
            //     signal.prevId = prevId;
            //     intentionBoard.AddIntention(signal);
            //     return signal.id;

            // }, 0)(signalArr) // -10


            break;
        default:
            console.log('---抱歉，我无法理解你输入的这句话的意思---');
        break;
  } 
}


// function convert(fromStr,constraint){

//     var result =Memory.queryMasterType(fromStr); 

//     return {
//         name:fromStr,
//         features :  {"_category_":result.shift()[1]}
//     }
// }
