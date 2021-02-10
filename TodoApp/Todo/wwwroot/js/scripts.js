
redirectToCheckout = function (sessionId) {
  var stripe = Stripe('pk_test_51IIUm0KsuYyFXhSvPIN8vpVEOwJuLMLVqoqBEwPVOXO3RC2Rh8CTRs2kxWbK51SQWsR8mBvAIltGDMY0bjheLavT00NKFlsOxO');
  stripe.redirectToCheckout({
    sessionId: sessionId
  });
};