new IdealPostcodes.Autocomplete.Controller({
    api_key: "ak_k7knhox8QBps6Ez5uqGfWPpBROFvV",
    inputField: "#postCode",
    outputFields: {},
    onAddressRetrieved: address => {
        const result = [
            address.line_1,
            address.line_2,
            address.line_3,
            address.post_town,
            address.postcode,
        ].filter(elem => elem !== "")
            .join(",");

        $("#output_field").html("<p> Selected postcode: " + result + "</p>");
    }
});