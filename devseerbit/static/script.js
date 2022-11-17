window.onload = (function(){
    console.log(typeof($('#am').html()));

    function paywithSeerbit() {
        SeerbitPay ({
        //replace with your public key
        "public_key": "SBTESTPUBK_bEcvQMD2fF0rTVV68cuVpjfsTf113Ho4",
        "tranref": new Date().getTime(),
        "currency": "NGN",
        "country": "NG",
        "amount": $('#am').html(),
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
        };

        $('button').click(function() {
            console.log(typeof($('#am').html()));
            paywithSeerbit();
            console.log(typeof($('#am').html()));          
        });
})