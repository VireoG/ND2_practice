import 'bootstrap-autocomplete';
import './events.index.js';

$('.basicAutoComplete').autoComplete({
    resolverSettings: {
        url: 'api/v1/filters/autocomplete'
    },
});

let data;
$("#inputsearch").on("change", () => {filt.search = $(this).val();})

$("#inputsearch").keydown(function () {
    pagin();
});
