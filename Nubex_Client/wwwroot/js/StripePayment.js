redirectToCheckout = function (sessionId) {
    var stripe = Stripe("pk_test_51LJ6W6Hb7LkPW4wMAIHhRrPrjcfGScwWMGNw0sgs2iQ4kN9xFPRi79hOwMYZ52xiSPPl2JA9739qgRoHz4n53NBF001LibtWHZ");
    stripe.redirectToCheckout({ sessionId: sessionId });
}