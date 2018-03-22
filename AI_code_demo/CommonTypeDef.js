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


//此词典/枚举用于标记信号对象【 Signal 】的类型，不同类型有不同的处理方式（有的类型有特定的触发器处理）
var SignalTypeDef ={
    CharInSentence:"一句话里面的单个字符",
    Task_SentenceSplitWord:"任务：句子的分词",
    WordAggregation:"聚合成了词语",
    SingleCharWord:"这是一个单字词语",
    WordExpect:"期待/预测是这个词",
    SentenceIsEnd:"表示一句话的结束",
    ConceptModel:"表示此信号是个概念模型", //当信号是ConceptModel时，表示 sign.Data属性是个model对象，该对象有modelType属性用以区分概念模型的类型
    TestTempType:"临时使用的，没有特殊意义",
    TestTempType22:"22临时使用的，没有特殊意义",


}

var ConceptModelTypeDef ={
    Question:"疑问概念模型",

}

var DataTagsDef ={
    GrammarPraseFinish :"语法分析完成",
    SemanticsPraseFinish :"语义分析完成",
    MemoryQueryFinish : "记忆库查询完成",
    SayWordFinish:"输出内容完成"

  

}

module.exports={
    ModelType       :   ModelTypeDef,
    MessageType     :   MessageTypeDef,
    TriggerTags     :   TriggerTagsDef,
    DataTags        :   DataTagsDef,
    SignalType      :   SignalTypeDef,
    ConceptModelType:   ConceptModelTypeDef
};



