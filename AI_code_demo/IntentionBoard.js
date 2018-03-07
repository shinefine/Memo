//意向公告板



var intentionList= [];
var triggerList = new Set();  //触发器不要重复添加


var taskId =1;
class Task{
    constructor(s,trace_task){
        this.msgData = s;        
        this.id = taskId;
        taskId = taskId + 1;

        this.trace_task = trace_task;
    }
}

var intentionBoard = {

    //增加一个任务
    AddIntention(s,trace_task){
        intentionList.push(s);

        var task = new Task(s,trace_task);

        //发出广播给各个触发器
        triggerList.forEach(tig=>tig.onMessage(task))

    },

    //添加触发器
    AddTrigger(t){
        t.feedbackObj = this; //如果不写这句,则触发器无法反馈结果出来,那么我们在公告板上发布的任务永远无法完成(得不到结果)
        triggerList.add(t);
    },

    //收到触发器发来的完成消息
    onTriggerCompleted(task,result){
        //再次发布一个公告,这个公告消息会由一个特殊的触发器来处理,

        this.AddIntention(result,task);

    }

    

    

}


module.exports = intentionBoard;