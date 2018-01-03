//本代码演示了调用百度ai平台的api 处理自然语言（得到语句词语的词性信息）
//需要安装百度 nlp sdk 包  （npm install baidu-aip-sdk）
//参见 http://ai.baidu.com/docs#/NLP-Node-SDK/top


var AipNlpClient = require("baidu-aip-sdk").nlp;

// 设置APPID/AK/SK
var APP_ID = "10571965";
var API_KEY = "rKO5KSNvfSnaqYzgQjD1RZId";
var SECRET_KEY = "0Wy30aXviQ5WLUcpGxplIefgR7OOX6kz";

// 新建一个对象，建议只保存一个对象调用服务接口
var client = new AipNlpClient(APP_ID, API_KEY, SECRET_KEY);


var text = "和尚未结婚";




function parseSentense(sentense){
    //  调用百度的词法分析api
    client.lexer(text).then(function (jsonResult) {
        
        jsonResult.items.forEach(i=>convertJson_Baidu(i));
        
        console.log(jsonResult);

    })
    .catch(function (err) {
        // 如果发生网络错误
        console.log(err);
    });
}

var j = {
    "log_id": 4489339945420123000,
    "text": "百度是一家高科技公司",
    "items": [{
        "loc_details": [],
        "byte_offset": 0,
        "uri": "",
        "pos": "",
        "ne": "ORG",
        "item": "百度",
        "basic_words": ["百度"],
        "byte_length": 4,
        "formal": ""
    }, {
        "loc_details": [],
        "byte_offset": 4,
        "uri": "",
        "pos": "v",
        "ne": "",
        "item": "是",
        "basic_words": ["是"],
        "byte_length": 2,
        "formal": ""
    }, {
        "loc_details": [],
        "byte_offset": 6,
        "uri": "",
        "pos": "m",
        "ne": "",
        "item": "一家",
        "basic_words": ["一", "家"],
        "byte_length": 4,
        "formal": ""
    }, {
        "loc_details": [],
        "byte_offset": 10,
        "uri": "",
        "pos": "n",
        "ne": "",
        "item": "高科技",
        "basic_words": ["高", "科技"],
        "byte_length": 6,
        "formal": ""
    }, {
        "loc_details": [],
        "byte_offset": 16,
        "uri": "",
        "pos": "n",
        "ne": "",
        "item": "公司",
        "basic_words": ["公司"],
        "byte_length": 4,
        "formal": ""
    }]
}


//百度api的词性代码说明见 http://ai.baidu.com/docs#/NLP-Node-SDK/top 词法分析--词性缩略说明
var dict_wordKind = {
    n:	'普通名词',	f:	'方位名词',	s:	'处所名词',	t:	'时间名词',
    nr:	'人名',	ns:	'地名',	nt:	'机构团体名',	nw:	'作品名',
    nz:	'其他专名',	v:	'普通动词',	vd:	'动副词',	vn:	'名动词',
    a:	'形容词',	ad:	'副形词',	an:	'名形词',	d:	'副词',
    m:	'数量词',	q:	'量词',	r:	'代词',	p:	'介词',
    c:	'连词',	u:	'助词',	xc:	'其他虚词',
}


parseSentense(text);

//j.items.forEach(i=>{convertJson_Baidu(i)});



//console.log (dict_wordKind['']|| 'N/A')

//转换baidu api 返回的json 对象 
function convertJson_Baidu(baidu_JsonObj)
{   
        baidu_JsonObj["字词文本"] = baidu_JsonObj.item;
   // console.log(dict_wordKind[jsonObj.pos])
        baidu_JsonObj["词性"] = dict_wordKind[baidu_JsonObj.pos]||baidu_JsonObj.pos;
 

        delete baidu_JsonObj.pos;
        delete baidu_JsonObj.item;
        delete baidu_JsonObj.loc_details;
        delete baidu_JsonObj.byte_offset;
        delete baidu_JsonObj.uri;
        delete baidu_JsonObj.ne;
        delete baidu_JsonObj.byte_length;
        delete baidu_JsonObj.formal;
    
}
