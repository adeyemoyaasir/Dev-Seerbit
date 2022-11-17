function PayMoney(){
    let amountid = document.getElementById('amount');
    if (amountid.value) {
        paywithSeerbit()
    }
    else {
        alert('fill the amount');
    }

}

function paywithSeerbit() {
    SeerbitPay ({
    //replace with your public key
    "public_key": "SBTESTPUBK_bEcvQMD2fF0rTVV68cuVpjfsTf113Ho4",
    "tranref": new Date().getTime(),
    "currency": "NGN",
    "country": "NG",
    "amount": '500',
    "email": "test@emaildomain.com",
    //optional field. Set to true to allow customer set the amount
    "setAmountByCustomer": false,
    "full_name": "John Doe", //optional
    "callbackurl": "http://yourdomain.com",
    },
    function callback(response, closeModal) {
     console.log(response) //response of transaction
    },
    function close(close) {
     console.log(close) //transaction close
    })
    }