let  typesDef  = require("./CommonTypeDef");
var  util = require('./util');  //要使用日志 对象 Logger

let  messageType = typesDef.MessageType;
let  triggerTags = typesDef.TriggerTags;
let  dataTags    = typesDef.DataTags;


let logger = new util.Logger(util.loggerType.TriggerLogger)

class Trigger{    
    constructor(name,tags,active_rule,active_func,feedbackObj){
        this.name = name;
        this.tags = tags;
        this.active_func = active_func;        
        this.active_rule = active_rule;        
        this.feedbackObj = feedbackObj;
    }

    //外部通知我有了新的消息，
    onMessage(task){      

        //我是否会被激活取决于消息的类型，若是我被激活，则执行相关的函数
        if(this.active_rule(task.msgData)){

            logger.message(`触发器${this.name}激活`); //----日志

            var result = this.active_func(task.msgData);

            this.complete(task,result);
    
        }
    }


    complete(task,result){               

        result.dataType = "undef";
        if(this.hasTag(triggerTags.TypeIsGrammarPrase)){
            result.dataType = dataTags.GrammarPraseFinish;
            
        }
        if(this.hasTag(triggerTags.TypeIsDoMemoryQuery)){
            result.dataType = dataTags.MemoryQueryFinish;
        }
        if(this.hasTag(triggerTags.TypeIsSayWord)){
            result.dataType = dataTags.SayWordFinish;
        }

        if(this.feedbackObj){

            this.feedbackObj.onTriggerCompleted(task,result);
        }
        
    }

    hasTag(tag){
        return this.tags.includes(tag);
    }
}



module.exports=Trigger;