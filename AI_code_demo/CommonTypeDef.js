var ModelTypeDef={
    "近义关系":"近义关系",
    "字词模型":"字词模型",
    "常识约束":"常识约束"
}


var words =[
    '人','人类','是','生物',    
]
var MessageTypeDef ={
    queryMasterType : "查询主体的类型"
    
}

//定义触发器的类型,行为,
var TriggerTagsDef ={
    ShouldFeedBack :"行为完成后应向外界反馈结果",
    NotFeedBack :"不需要向外界反馈信息",
    TypeIsGrammarPrase :"类型是语法分析",
    TypeIsSemanticsPrase: "类型是语义分析",
    TypeIsDoMemoryQuery :"类型是进行记忆库搜索行为",
    TypeIsSayWord : "输出信息"

}

var DataTagsDef ={
    GrammarPraseFinish :"语法分析完成",
    SemanticsPraseFinish :"语义分析完成",
    MemoryQueryFinish : "记忆库查询完成",
    SayWordFinish:"输出内容完成"

  

}

var TypesDef = {
    ModelType:ModelTypeDef,
    MessageType:MessageTypeDef,
    TriggerTags:TriggerTagsDef,
    DataTags:DataTagsDef
}

module.exports=TypesDef;



