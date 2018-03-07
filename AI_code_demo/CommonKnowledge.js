

var a={
  name:'苹果',
  features:{
    '_category_':'水果',
    '颜色':'绿色',
  },
}
var b={
  name:'香蕉',
  features:{
    '_category_':'水果',
    '颜色':'黄色',
  },
}
var c ={
  name:'水果',
  features:{
    '_category_':'食物',    
  },
}
//判断两个对象有哪些特性是相同的,哪些是不同的
function senseSameAndDiff([a,b]){
  let same=[];
  let diff=[];

  same.push( "features._category_");

  diff.push("name");
  diff.push("features.颜色");

  return {
    same:same,
    diff:diff,
  };
}

//判断两个对象 a,b各自的两个特性 aProp,bProp 是否是相同值
function isSameFeature(a,b,a_prop,b_prop){
  let aPropValue = getObjPropValue(a,a_prop);
  let bPropValue = getObjPropValue(b,b_prop);

  return aPropValue == bPropValue;

}
//给定一个对象和一个属性名称,返回其属性值,属性名称可以是嵌套的,比如,若要获取a.b.c.d的值,则 参数o=a, propNameArr =["b","c","d"]
function getObjPropValue(o,propName){

    function _getObjPropValue(o,propNameArr){
    if(propNameArr.length>1){
      return _getObjPropValue(o[propNameArr.shift()],propNameArr)
    }
    return o[propNameArr.shift()];
  }
   return _getObjPropValue(o,propName.split("."))
  
}

var hasFeature = function(obj,featureName){

}
//=================定义推导规则==============================

//如果苹果是水果,水果是食物.那么,苹果是食物.
//when  x 具备特性y,  when  Y 具备特性 z , ---> x 具备特性 z

//rule1 这个函数相当于定义了一个规则 objX所属的种类值是否等于 objY的名字值

function rule1(objX,objY){

  return getObjPropValue(objX,"features._category_") ==  getObjPropValue(objY,"name") ;

}


var commonKnowledgeLib = {

    derivedCommonKnowledge_category(obj1,obj2){
        //要优化，
        //参数可以允许多个对象，改成传入一个数组
        // 下面的if 判断应该改写成 组合函数 array.combination().forEach(c=>{.....}) 
      
      
        let targetX = null,targetY = null;
        if(rule1(obj1,obj2)){
          targetX = obj1;
          targetY = obj2;
      
        }
        if (rule1(obj2,obj1)){
          targetX = obj2;
          targetY = obj1;
        }
      
        if(targetX && targetY){
          return {
            name:targetX.name,
            features:{'_category_': getObjPropValue(targetY,"features._category_")}
        
          }
         
        }
        return null;
        
      }
      

}
module.exports=commonKnowledgeLib;

//=======================================test================
//console.log(isSameFeature(c,a,"name","features._category_"))
//console.log(derivedCommonKnowledge_category(a,c))