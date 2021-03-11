



var PayPalRedirectToCheckout = function () {
//  debugger

  paypal.Buttons({
    createOrder: () => {
      return fetch('https://localhost:5001/api/PayPal/CreateOrder', {
        method: 'post',
        headers: {
          'content-type': 'application/json'
        }
      }).then( (res) => {
        return res.json();
      }).then( (data) => {
        return data.payload.id; // Use the key sent by your server's response, ex. 'id' or 'token'
      });
    },
    onApprove: (data) => {
      return fetch("https://localhost:5001/api/PayPal/CaptureOrder/"+data.orderID, {
        //headers: {
        //  'content-type': 'application/json'
        //},
        method: 'post',
        headers: {
          'content-type': 'application/json'
        }
        //body: JSON.stringify({
        //  orderID: data.orderID
        //})
      }).then( (res) => {
        return res.json();
      }).then((details) => {
        alert('Transaction funds captured from ' + /*details.Order.Payer.Name.GivenName*/details.payer_given_name);
        //result.Order.Payer.Name.GivenName
      })
    }
  }).render('#paypal-button-container');


//<script>
//paypal.Buttons({
//  createOrder: function () {
//    debugger
//      return fetch('https://localhost:5001/api/PayPal/CreateOrder/ data.orderID*', {
//      method: 'post',
//      headers: {
//        'content-type': 'application/json'
//      }
//    }).then(function (res) {
//      debugger
//      return res.json();
//    }).then(function (data) {
//      //debugger
//      return data.id; // Use the key sent by your server's response, ex. 'id' or 'token'
//    });
//  },

//  onApprove: function (data) {
//    debugger

//    return fetch('https://localhost:5001/api/PayPal/CaptureOrder/id='+ data.orderID/*'/PayPalTestImplementation/CaptureOrder?orderId=' + data.orderID*/, {
//      method: 'post',
//      headers: {
//        'content-type': 'application/json'
//      },
//      body: JSON.stringify({
//        orderID: data.orderID
//      })
//    }).then(function (res) {
//      return res.json();
//    }).then(function (details) {
//      debugger
//      alert('Transaction funds captured from ' + details.givenName + " " + details.surName);
//    })
//  }
//}).render('#paypal-button-container');
};
  //</script>