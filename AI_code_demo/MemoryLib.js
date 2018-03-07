/*
*************模块的作用****************************
记忆库 存储各种信息，字词，短语，规则，常识....


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
var wordLib=[]


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

    }

    
    

}


module.exports=memoryLib;