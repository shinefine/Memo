/*
使用RX 框架实现的公告板

*/
const R  = require('ramda');
var  Rx = require('rxjs/Rx');
let  typesDef  = require("./CommonTypeDef");
var  util = require('./util');  //要使用日志 对象 Logger

var  Signal = util.Signal;

var  Memory = require('./MemoryLib');

let  messageType = typesDef.MessageType;
let  triggerTags = typesDef.TriggerTags;
let  signalType  = typesDef.SignalType;
let  dataTags    = typesDef.DataTags;


let logger = new util.Logger(util.loggerType.TriggerLogger)


var current_subscription;
var boardSubject = new  Rx.Subject();
boardSubject.subscribe(Memory.addSignal);
var stream = boardSubject.map(x=>x);

var intentionBoard = {

    //增加一个任务
    AddIntention(signal){
        
        //Memory.addSignal(signal);

        boardSubject.next(signal);        
    },

    //添加触发器
    AddTrigger(tig){
        //tig.feedbackObj = this; //如果不写这句,则触发器无法反馈结果出来,那么我们在公告板上发布的任务永远无法完成(得不到结果)
        console.log("注册触发器"+ this);
        //触发器订阅公告板流
       
        stream.filter(shouldActiveTrigger(tig)) //过滤流中的信号，若是该触发器可以处理此信号，则进行处理
        // .do(x=>{
        //                                                 if(tig.name =="T3"){
        //                                                 logger.message(`触发器${tig.name}激活`)
        //                                                 }
        //                                             }) //----日志)
                                                    .map(processTask(tig))//task-> new task  //该触发器被触发，执行后返回一个结果，processTask 将结果包装为新的task对象返回
                                                    .subscribe(intentionBoard.AddIntention) //新的task对象被抛进流中（将会被其它符合条件的触发器处理）  
                                                    //【bug已修正】注意这里不能使用this.AddIntention 而要写成 intentionBoard.AddIntention
                                                    // 当外部代码是 arr.map(func)风格时，this被绑定到了global对象上
                                                    // 若外部代码是 arr.map(a=>func(a))时，则this 绑定的值是正确的。

        // stream = stream.merge(
        //                                         stream
        //                                             .filter(tig.active_rule)  
        //                                             .do(x=>logger.message(`触发器${tig.name}激活`)) //----日志)
        //                                             .map(processTask(tig))  //task-> new task
        //                                     )

    },
    AddTrigger_WordAggregation(tig){
        //stream.filter(sig=>sig.SignalType == signalType.CharInSentence).map(tig.active_func(stream)).subscribe(signalArr => signalArr.map(intentionBoard.AddIntention));
        stream.filter(tig.active_rule).map(tig.active_func({masterStream:boardSubject,memory:Memory})).subscribe(signalArr => signalArr.map(intentionBoard.AddIntention));
    },
}

var shouldActiveTrigger = R.curry(function(tig,signal){
    if(!signal ){
        console.log("error");
    }
    return tig.active_rule(signal.data)
});

var processTask =  R.curry(function (tig,signal){
    //var result  = tig.active_func(task.msgData);
    var result  = tig.active_func(signal.data);
    return wrapperToTask(signal,result,tig);
    
});

function wrapperToTask(signal,result,tig){                        

    result.dataType = "undef";
    if(tig.hasTag(triggerTags.TypeIsGrammarPrase)){
        result.dataType = dataTags.GrammarPraseFinish;
        
    }
    if(tig.hasTag(triggerTags.TypeIsDoMemoryQuery)){
        result.dataType = dataTags.MemoryQueryFinish;
    }
    if(tig.hasTag(triggerTags.TypeIsSayWord)){
        result.dataType = dataTags.SayWordFinish;
    }

    return new Signal(result,signal.id);       
}



module.exports = intentionBoard;