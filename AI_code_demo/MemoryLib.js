/*
*************模块的作用****************************
记忆库 存储各种信息，字词，短语，规则，常识....
记忆库 可以动态的创建出新集合一共特定目的的存储和查询

*************本模块提供的接口:**********************
humanText          model->str
modelType          model->str
modelExplain       model->str (尚未实现)

*********Code Example 如何使用本模块：**************
1.外部代码通过
var H = require('./help');  引用此模块

2.外部代码需要先定义某个模型
var sr ={
        similarWordsGroup:["空间","地点","区域"],
        limitCondition: "总是", //在什么情况下成立,always 表示始终成立


        //humanExplain:"[空间],[地点],[区域]总是近义关系"  ，这个属性可以不用定义，本库的humanText()会根据模型类型生成出对应的文本值
                                                            若模型对象定义了humanExplain 属性，则 humanText() 会直接使用这个属性值作为返回值
    }

3. 外部代码使用本模块定义的方法
 console.log(H.humanText(cs1)); 查看结果

*/

//wordLib 保存的对象应该是 全部的字词和其对应的概念模型（关系），这里只保存了字词，应该改掉
var wordLib=["苹果","水果","果醋","苹果醋",,"不是","是不是","是不","种类","食物","一种",

];

var wordLinkPatternList ={
    "是不是":[122344,5543543,66654654,77765765],
    "苹果":[344353],
    "水果":[454456455]
};




var patternModelList=[
    {
        id:1,
        description_masters: ["[something1]","[something2]"],
        mastersRelation:"是"
    },



    
]


let pattern1 = {
    id:1,
    patternType:"锚定到一个具体词语上",
    description:"求陈述事实的真假值",
    anchorWord:"是不是", //关键词,锚定词
}

let pattern2={
    id:2,
    patternType:"Before,在某个关键点之前的信号",
    anchorPattern:1,

}


let pattern3={
    id:3,
    patternType:"After,在某个关键点之后的信号",
    anchorPattern:1,
}


let pattern4 ={
    id:4,
    patternType:"combine,组合"
}

let pattern5 ={
    id:5,
    patternType:"求值",

}

let pattern6 ={
    id:6,
    patternType:"搜索",
    description:"在 是不是 信号锚定点之前查找一个 范畴类型的概念",
    range:2,
    condition:999,
}
 
let pattern999={
    id:999,
    patternType:"表示某个模式具备特性",
    value:"__category__",
}



/*是不是 这个聚合词对应到了一个模式,这个模式会引发一系列的查询操作,

以 是不是作为锚定点,在其之前找一个 范畴类型的概念(对应的字符信号)
在其之后找一个范畴类型的概念(对应的字符信号)

如果两个都已经找到,则说明这句话表达了两个范畴概念之间的种属关系(疑问)
那么后续激发模式会进行数据库查询操作.






 */

var signalList =[];


var allCategoryList={
    "单字词语":"是不好行吧呢果金木水火土".split(''),
    "多字词语":["苹果","水果","果醋","苹果醋","不是","是不是","是不","种类","食物","一种","果汁","水果汁","是什么类型"]

}

/*
类型库是用来存储 某个概念主体的类型是什么  这样一种信息的

比如： 苹果是一种水果
      水果是一种食物
      苹果是一种食物
      相当于在图数据库中，苹果，水果 ，食物这三个节点互相之间两两关联， 当然这种关联关系要分主体和客体，因为我们不能说 食物是一种苹果

当我们表达 xxx 是一种 yyy 时， (xxx 和 yyy 的连接关系被存储与 类型库中),我们实际上表达的意思是 xxx的【类型】是 yyy

所以 typeLib 存储的数据应该是这样的 [['苹果,食物'],['水果,食物'],['苹果,水果'],.....]
*/
var typeLib=[]

var memoryLib = {


    AllSignals:signalList,
    AllWords:wordLib,

    querySignals(f){
        return signalList.filter(f);
    },

    //学习一个词语，将其添加进字词库中
    addWord(word){
        if(!wordLib.includes(word.trim()))
        {
            wordLib.push(word.trim());            
        }
    },

    //学习一个主体的所属类型
    studyType(master,typeText)
    {
        typeLib.push([master,typeText]);
    },

    //查询主体的所属类型

    queryMasterType(master,constraint){
        var r = typeLib.filter(i=>i[0] == master);        
        if(constraint){
            r = r.filter(i=>i[1] == constraint);
        }
        return r;

    },

    //查询一个词，
    queryWord(word){
        return this.queryWordStartWith().filter(w=>w.length == word.length)
    },

    queryWordStartWith(word){
        return allCategoryList["多字词语"].filter(w=>w.startsWith(word));
    },
    
    querySingleCharWord(charWord){
        return allCategoryList["单字词语"].filter(w=>w == charWord)
    },

    addSignal(signal)
    {
        signalList.push(signal);
    },
    querySignalById(signalId){
        return signalList.filter(sig=>sig.id == signalId);
    },
    querySignalBy(f){
        return signalList.filter(f)
    }

}


module.exports=memoryLib;