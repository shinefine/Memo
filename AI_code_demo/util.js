var loggerConfig ={
    showTriggerMsg:true,
}

var loggerType = {
    TriggerLogger:"trigger_logger"
}

class Logger{
    constructor(type){
        
        this.loggerType  = type;
    }
    message(msg){
        if(this.loggerType == loggerType.TriggerLogger && loggerConfig.showTriggerMsg){
            console.log(msg);
        }
    }
}



//计数器
var counter = {
    cnt:0,
    increase:function(){
        this.cnt++;
        return this.cnt;
    },
}

class Signal{

    newId(){
        return counter.increase();
    }

    constructor(signalData,sourceSignalId,signalType){

        this.id          = this.newId()
        this.timestamp   = new Date().getTime();
        this.data        = signalData;                
        this.sourceSignalId  = sourceSignalId;       
        this.SignalType = signalType;
        ///this.charIndex = xxxx;
    }  
    
    
    // toString(){
        //当前vscode调试器不支持自定义格式输出
    //     return `${ this.SignalType }:${this.data.toString()} [${this.id} <-- ${this.sourceSignalId}]`
    // }
}



// 作者：文兴
// 链接：https://www.jianshu.com/p/04b1d88dabf2

//给一个对象扩展新的属性，如果使用了---immutable.js---库，则不再需要此函数
var extend = (function() {
    var isObjFunc = function(name) {
        var toString = Object.prototype.toString
        return function() {
            return toString.call(arguments[0]) === '[object ' + name + ']'
        } 
    }
    var   isObject = isObjFunc('Object'),
        isArray = isObjFunc('Array'),
        isBoolean = isObjFunc('Boolean')
    return function extend() {
        var index = 0,isDeep = false,obj,copy,destination,source,i
        if(isBoolean(arguments[0])) {
            index = 1
            isDeep = arguments[0]
        }
        for(i = arguments.length - 1;i>index;i--) {
            destination = arguments[i - 1]
            source = arguments[i]
            if(isObject(source) || isArray(source)) {
                //console.log(source)
                for(var property in source) {
                    obj = source[property]
                    if(isDeep && ( isObject(obj) || isArray(obj) ) ) {
                        copy = isObject(obj) ? {} : []
                        var extended = extend(isDeep,copy,obj)
                        destination[property] = extended 
                    }else {
                        destination[property] = source[property]
                    }
                }
            } else {
                destination = source
            }
        }
        return destination
    }
})()



module.exports = {extend: extend,
                    Logger          : Logger,
                    loggerType      : loggerType,
                    Signal          : Signal,
                  
                 };