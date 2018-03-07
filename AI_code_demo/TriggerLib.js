const Trigger = require('./Triggers');
const R  = require('ramda');
let  typesDef  = require("./CommonTypeDef");
var  util = require('./util'); //要使用extend 方法
var Memory = require('./MemoryLib');

let  messageType = typesDef.MessageType;
let  triggerTags = typesDef.TriggerTags;
let  dataTags    = typesDef.DataTags;

var triggerList = []


//定义各种触发器
//==============================
//判断句子里是否有关键词的触发器
var rule_sentenceContainKeyWord  = R.curry( function rule_sentenceContainKeyWord(keyword,sentence){
    return (typeof(sentence)=="string") && sentence.search(keyword) > 0 ;
});


var action_wrapMasterSlave = R.curry( function action_wrapMasterSlave(verbWord,sentence){

        var arr          = sentence.split( verbWord )
        var str_master   = arr[0];
        var str_slave    = arr[1];

        
        return {
            master  :str_master,
            slave   :str_slave,
            verb    : verbWord
        };

    }
);


var rule1 = rule_sentenceContainKeyWord("是不是");
var act1 = action_wrapMasterSlave("是不是");

var t1 = new Trigger("T1",
    [        
        triggerTags.TypeIsGrammarPrase
    ],
    rule1,
    act1
)
// 是什么类型

var rule2 = rule_sentenceContainKeyWord("是什么类型");
var act2 = action_wrapMasterSlave("是什么类型");

var t4 = new Trigger("T4",
    [        
        triggerTags.TypeIsGrammarPrase
    ],
    rule2,
    act2
)

//===============
function rule_2(data){
    return (typeof(data)=="object") && (data.dataType == dataTags.GrammarPraseFinish && data.verb == "是不是");
}

function active_func_Memory_QueryMasterType(data){    
    return util.extend(true,{},data, {memoryQueryResult: Memory.queryMasterType(data.master,data.slave)});    
}

var t2 = new Trigger("T2",
    [       
        triggerTags.TypeIsDoMemoryQuery
    ],
    rule_2,
    active_func_Memory_QueryMasterType
)


//===============
function rule_5(data){
    return (typeof(data)=="object") && (data.dataType == dataTags.GrammarPraseFinish && data.verb == "是什么类型");
}


var t5 = new Trigger("T5",
    [       
        triggerTags.TypeIsDoMemoryQuery
    ],
    rule_5,
    active_func_Memory_QueryMasterType
)

//==============================

function rule_3(data){
    return (typeof(data)=="object") && (data.dataType == dataTags.MemoryQueryFinish && data.verb == "是不是");
}
function action_func_SayResult(data){    
    var result = data.memoryQueryResult;
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
        console.log(`${data.master}是一种${data.slave}`) ;
    }else{
        console.log(`${data.master}不是一种${data.slave}`)
    }   

    return {};

}


t3 = new Trigger("T3",
    [     
        triggerTags.TypeIsSayWord
    ],
    rule_3,
    action_func_SayResult
)


function rule_6(data){
    return (typeof(data)=="object") && (data.dataType == dataTags.MemoryQueryFinish && data.verb == "是什么类型");
}
function action_func_SayResult2(data){    
    var result = data.memoryQueryResult;
    if(result.length == 0){
        console.log(`我不知道 '${data.master}'是什么类型,我应该还没有学习这个知识`);
    }else{
        var msg = `${data.master}是${result.shift()[1]}类型`
        if(result.length>0){
            msg += `,它同时也是${result.map(i=>i[1]).join(",")}`;
        }
        console.log(msg)
    }

    return {};

}


t6 = new Trigger("T6",
    [     
        triggerTags.TypeIsSayWord
    ],
    rule_6,
    action_func_SayResult2
)












triggerList= [t1,t2,t3,t4,t5,t6];


module.exports=triggerList;