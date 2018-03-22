let  typesDef  = require("./CommonTypeDef");
var  util = require('./util');  //要使用日志 对象 Logger

var Signal = util.Signal;

let  messageType = typesDef.MessageType;
let  triggerTags = typesDef.TriggerTags;
let  dataTags    = typesDef.DataTags;


let logger = new util.Logger(util.loggerType.TriggerLogger)


/*
    使用方式
    var tig = new Trigger(
        "触发器名字"
        ,[ triggerTags.TypeIsGrammarPrase]   //使用枚举值来标记该触发器的功能，用途等特性
        ,some_func   //返回 true/false 值的函数，该函数接收一个 sigData 参数 对应于 Signal 对象的 data 属性
        ,some_func  //做某件事的函数，返回值任意，且该返回值会被外部包装成 Signal 对象重新塞入 主流中。
    )

*/
class Trigger{    
    constructor(name,tags,active_rule,active_func){
        this.name = name;
        this.tags = tags;       
        this.active_rule = active_rule;    
        this.active_func = active_func;            
       
    }
    
    hasTag(tag){
        return this.tags.includes(tag);
    }
}



module.exports=Trigger;