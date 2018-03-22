

const R  = require('ramda');
var  Rx = require('rxjs/Rx');
console.log("rx");
var subject = new  Rx.Subject();

subject.next(1);
sub  = subject.bufferCount(3).subscribe(x=>{
    console.log(x.join(''));
    sub.unsubscribe();
});
console.log("===============kkkkkkk")
subject.next(2);
subject.next(3);
subject.next(4);

subject.next(5);
subject.next(6);
subject.next(6);
subject.next(6);
subject.next(6);
subject.next(6);
subject.next(6);












console.log("=========================================");


let  a=[].concat(111).concat([22,33])
console.log(a);
console.log(R.range(1,10));
var split = R.curry((char, str) =>
{
    console.log(str);
    return str.split(char);
}
 );

var split_a = split("a");


var fff = R.compose(split("a"),split("b"));

console.log(fff("eearrb664a53"));




function  *simpleCombination(arr){
    yield [arr[0],arr[1]];
    return [arr[1],arr[0]];

}


let getSomeCombinationValue  =   simpleCombination([1,2]);

while(1)
{
    let v = getSomeCombinationValue.next();
    let [PHasFeature,PCategoryBelong] =v.value;
    console.log([PHasFeature,PCategoryBelong]);
    if(v.done){
        break;
    }

}
console.log("ok")