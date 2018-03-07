
/*
*************模块的作用****************************
模式库

定义了基本模式和高阶模式（处理模式的模式）


*************本模块提供的接口:**********************


*********Code Example 如何使用本模块：**************
1.外部代码通过
var P = require('./Pattern');  引用此模块

2.外部代码需要先定义某个模型

3. 外部代码使用本模块定义的方法


*/

//该用能用于进行 特性转移 推导, 接收两种类型的概念,
//一种是某概念(C1)具备某种特性(F),一种是某概念(C2)种类属于某概念(C3)
//并且 当C1 == C3 时,  那么可以推导出 C2具备某种特性(F)
//例子: 水果具备味道, 苹果属于水果 --> 苹果具备味道

//高阶模式_泛化范畴具备的特性传递给特化范畴
function HighPattern_DeliverFeature_To_SpecialCategory (modelList){

    let getSomeCombinationValue  =   simpleCombination(modelList);
    while(1)
    {
        let v = getSomeCombinationValue.next();
        let [PHasFeature,PCategoryBelong] =v.value;
        
        if((PHasFeature.relation =="具备特性")
         && (PCategoryBelong.relation =="种类所属")
         && (PHasFeature.master == PCategoryBelong.slave))
        {
            return {
                master:PCategoryBelong.master,
                relation: PHasFeature.relation,
                slave: PHasFeature.slave
            } //该模式成功
        }        

        if(v.done){
            break;
        }

    }
    return {}; //该模式不能成功

}

function HighPattern_Category_Conclude(modelList){
    let getSomeCombinationValue  =   simpleCombination(modelList);
    while(1)
    {
        let v = getSomeCombinationValue.next();
        let [PCategoryBelong1,PCategoryBelong2] =v.value;
        
        if((PCategoryBelong1.relation =="种类所属")
         && (PCategoryBelong2.relation =="种类所属")
         && (PCategoryBelong1.slave == PCategoryBelong2.master))
        {
            return {
                master:PCategoryBelong1.master,
                relation: PCategoryBelong1.relation,
                slave: PCategoryBelong2.slave
            }
        }        

        if(v.done){
            break;
        }

    }
    return {};//该模式不能成功
}



class PropertyNameDesc{
    constructor(propName){
        this.propName= propName;     
       
    }
}

class PropertyValueCompareDesc{
    constructor(propValueSource,propValueTarget,expect="equal"){
        this.propValueSource =propValueSource;   
        this.propValueTarget =propValueTarget;   
        this.expect =expect;   
    }

}

class PropertyValueDesc{
    constructor(propNameDesc,propValue){
        this.propName =propNameDesc;   
        this.propValue =propValue;   
        
    }

}

//如果[概念1]具备特性[特性1],并且[概念2]种类所属[概念1],那么[概念2]具备特性[特性1]
//如果{模型1 的[概念1]具备特性[特性1]},并且{模型2的 [概念2]种类所属[概念1]}  ,那么[概念2]具备特性[特性1]
pattern_meta ={
    
inputObjNumber:2,
condition: [
                    new PropertyValueCompareDesc(new PropertyNameDesc["param_01","relation"],"具备特性"),
                    new PropertyValueCompareDesc(new PropertyNameDesc["param_02","relation"],"种类所属"),
                    new PropertyValueCompareDesc(new PropertyNameDesc["param_01","master"],new PropertyNameDesc["param_02","slave"])           
                
            
],


   


conclude:[{
    master: "[概念2]",
    relation: "具备特性",
    slave:"[特性1]"
}
    
}

]



//如果[概念1]种类所属[概念2],并且[概念2]种类所属[概念3],那么[概念1]种类所属[概念3]






//结合 上面两个模式定义函数可以推导出下面的事实

//      食物可以吃,水果属于食物,苹果属于水果-->   水果可以吃,苹果属于水果--> 苹果可以吃
//                                       -->   食物可以吃,苹果属于食物--> 苹果可以吃            










//简略实现，要求参数只能是两个元素的数组
//【问题】该函数不够灵活
//----默认返回数组元素的2元组合值 （不能返回3元组合，1元组合....）
//----默认数组只包含有两个元素(传递1个元素的数组不会报异常，传递多个元素的数组则被忽略其余元素)
function  *simpleCombination(arr){
    yield [arr[0],arr[1]];
    return [arr[1],arr[0]];

}


//----------------------------test code---------

var model1 ={
    master  :"食物",
    relation:"具备特性",
    slave   :"味道"

}

var model2 ={
    master  :"水果",
    relation:"种类所属",
    slave   :"食物"

}

var model3 ={
    master  :"苹果",
    relation:"种类所属",
    slave   :"水果"

}


console.log(HighPattern_DeliverFeature_To_SpecialCategory([model1,model2]))

console.log(HighPattern_Category_Conclude([model3,model2]))