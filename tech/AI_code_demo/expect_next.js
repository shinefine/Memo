//Լ������
class Constraints{
    constructor(condition){
        this.condition= condition;        
    }
}


//ʱ����� ���¼��Ⱥ�˳�����У������ϵ
class Sequence{
    constructor(base,next){
        this.base= base;       
        this.next =next; 
    }   
}

  

//ͬ�Ƚ������(����/���������ĳ�������һ����)

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

//-----------------���Կ��--------------

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

