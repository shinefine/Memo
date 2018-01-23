
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

module.exports.expect =expect;