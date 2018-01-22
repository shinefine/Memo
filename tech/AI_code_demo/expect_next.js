//约束概念
class Constraints{
    constructor(condition){
        this.condition= condition;        
    }
}


//时许概念 ，事件先后顺序，序列，因果关系
class Sequence{
    constructor(base,next){
        this.base= base;       
        this.next =next; 
    }   
}

  

//同等近义概念(两个/多个概念在某方面具有一致性)

class Similar{
    constructor(){
        this.guid = new Guid();
    }
    

}


function express(m,context)
{

    return "aaa";
}

function test()
{
    var m={};
    expect(express(m)).getString("aaa")
}
test()

//-----------------测试框架--------------

class TestObj{
    constructor(func_result){
        this.func_result= func_result;   
    }
    getString(expectStr){
       if(this.testValue(expectStr,this.func_result)){
            console.log("test ok");
       }else{
            console.log("test fail: expect string:"+ expectStr+ ",but actual value is "+ this.func_result)
       }
    }

    testValue(expect,actual){
        return expect==actual;
        
    }
}
function expect(value){
    return new TestObj(value);
}

