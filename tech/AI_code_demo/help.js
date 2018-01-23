/*
*************模块的作用****************************



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


var ModelTypeDef = require('./CommonTypeDef');
var help={
    humanText(model)
    {
        if ('humanExplain' in model)
        {
            return model.humanExplain
        }else
        {

            switch(this.modelType(model)){
                case ModelTypeDef.字词模型:
                    return model.text;
                case ModelTypeDef.常识约束:
                    return `[${this.humanText(model.master)}]-<${model.constraint}>-[${this.humanText(model.slave)}]`;
                case ModelTypeDef.近义关系:
                    return `[${model.similarWordsGroup.join()}]${model.limitCondition}${ModelTypeDef.近义关系}`;
                default:
                    return "无法解释此模型";
            }   
        }
    },
    
    modelType(model){    
        if ('type' in model)
        {
            return model.type
        }else{
            if('similarWordsGroup' in model){
                return ModelTypeDef.近义关系;
            }
            if('constraint' in model){
                return ModelTypeDef.常识约束;            
            }
            if('text' in model){
                return ModelTypeDef.字词模型;            
            }
            return "未知类型的模型";
        }
    }, 

}


module.exports=help;