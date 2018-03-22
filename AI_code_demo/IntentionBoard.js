//意向公告板
var Task =require('./util').Task;


var intentionList= [];
var triggerList = new Set();  //触发器不要重复添加




var intentionBoard = {

    //增加一个任务
    AddIntention(task){


        //发出广播给各个触发器
        triggerList.forEach(tig=>tig.onMessage(task))

    },

    //添加触发器
    AddTrigger(t){
        t.feedbackObj = this; //如果不写这句,则触发器无法反馈结果出来,那么我们在公告板上发布的任务永远无法完成(得不到结果)
        triggerList.add(t);
    },

}


module.exports = intentionBoard;