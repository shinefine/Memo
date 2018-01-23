//表达库

function express(model,context)
{
    let m = model;

    return "aaa";
}


//---------测试---------------

var T = require('./simpleTest');
var ModelTypeDef = require('./CommonTypeDef');
var H = require('./help');
function test()
{
    var m1={
        text:"四川",
        is_a:["地点","省份"]


    };
    var m2 ={
        text:"潮湿",
        is_a:["含水量"]        
    }

    //r1 通过对 cs1 进行变换操作（下降）可得
    var r1 =  {
        constraint:"具备特性",
        master: m1,
        slave:m2,

    }

    var x1={
        type: ModelTypeDef.字词模型,
        text: "空间"
    }
    var x2 ={
        text:"含水量"
    }
    //cs ： commonSense 常识
    var cs1={
        constraint: "具备特性",
        //master: m1,
        master: x1,
        slave:x2,
        //humanExplain:"[空间]-<具备特性>-[含水量]"
    }
    //sr: similarRelation 近义
    var sr ={
        similarWordsGroup:["空间","地点","区域"],
        limitCondition: "总是", //在什么情况下成立
        //humanExplain:"[空间],[地点],[区域]总是近义关系"
    }

    console.log(H.humanText(r1));

    // console.log(H.humanText(x1));
    // console.log(H.modelType(cs1));
    //T.expect(express(m)).getString("aaa")
}

test()



//test();