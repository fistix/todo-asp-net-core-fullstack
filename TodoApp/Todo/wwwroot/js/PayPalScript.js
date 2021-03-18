//One Time Payment function
var OneTimeCheckout = function () {

  paypal.Buttons({
    createOrder: () => {
      return fetch('https://localhost:5001/api/PayPal/CreateOrder', {
        method: 'post',
        headers: {
          'content-type': 'application/json'
        }
      }).then((res) => {
        return res.json();
      }).then((data) => {
        return data.payload.id; // Use the key sent by your server's response, ex. 'id' or 'token'
      });
    },
    onApprove: (data) => {
      return fetch("https://localhost:5001/api/PayPal/CaptureOrder/" + data.orderID, {
        method: 'post',
        headers: {
          'content-type': 'application/json'
        }
        //,body: JSON.stringify({
        //  orderID: data.orderID
        //})
      }).then((res) => {
        return res.json();
      }).then((details) => {
        alert('Transaction funds captured from ' + /*details.Order.Payer.Name.GivenName*/details.payer_given_name);
        //result.Order.Payer.Name.GivenName
      })
    }
  }).render('#paypal-button-container');

};


//Subscription function
var Subscription = function (PlanId) {

  paypal.Buttons({
    style: {
      shape: 'pill',
      color: 'gold',
      layout: 'vertical',
      label: 'subscribe'
    },
    createSubscription: function (data, actions) {
      debugger
      return actions.subscription.create({
        //'plan_id': 'P-9XU85364167496542MBBELRA'
        'plan_id': PlanId
      });
    },
    onApprove: function (data, actions) {
      debugger
      return fetch("https://localhost:5001/api/PayPal/AttachSubscriptionIdToCustomer/" + data.subscriptionID, {
        method: 'post',
        headers: {
          'content-type': 'application/json'
        }
      }).then((dataa) => {
        alert('Your Subscription Id is:  ' + data.subscriptionID);
        //alert('Transaction funds captured from ' + /*details.Order.Payer.Name.GivenName*/details.payer_given_name);
        //result.Order.Payer.Name.GivenName
      })
      //alert(data.subscriptionID);
    }
  }).render('#paypal-button-container');

};
