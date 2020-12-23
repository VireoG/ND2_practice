import api from "./api.js";
import { Event, createSelectorItem } from "./items.js";

export const filt = {
    cities: [],
    venues: [],
    sortBy: 'Date',
    sortOrder: 'Ascending',
};

const selects = {
    cities: [],
    venues: []
};

const events = {
    event: []
};


async function loadCitySelector() {
    const data = await api.getCities();
    selects.cities = data;
    const selector = $("#cities");
    selector.empty().append($.map(data, createSelectorItem));
    selector.selectpicker('refresh');
}

async function loadVenueSelector(selects) {
    const data = await api.getVenues(selects);
    selects.venues = data;
    const selector = $("#venues");
    selector.empty().append($.map(data, createSelectorItem));
    selector.prop("disabled", false);
    selector.selectpicker('refresh');
}

$(document).ready(async function () {
    await loadCitySelector();
    pagin();
});

$("#cities").on('change', async function () {
    filt.cities = $(this).val();
    selects.cities = filt.cities;
    await loadVenueSelector(selects);
    pagin();
});

$("#venues").on('change', function () {
        filt.venues = $(this).val();
        pagin();
});

$("#sortby").on('change', function () {
    filt.sortBy = $(this).val();
    $("#sortby").selectpicker('refresh');
        pagin();
});

function pagin(search) {
    let num;
    $('#paged').pagination({
        dataSource: function (done) {
            $.ajax({
                url: `/api/v1/events`,
                type: 'GET',
                traditional: true,
                data: {
                    SortBy: filt.sortBy,
                    PageSize: filt.pageSize,
                    SortOrder: filt.sortOrder,
                    Venues: filt.venues,
                    Cities: filt.cities,
                    Search: search
                },
                success: function (responce, status, xhr) {
                    done(responce);
                    const count = xhr.getResponseHeader('x-total-count');
                    num = count;
                    events.event = responce;
                }
            });
        },
        totalNumber: num,
        pageSize: 3,
        showPageNumbers: true,
        showPrevious: true,
        showNext: true,
        showFirstOnEllipsisShow: true,
        showLastOnEllipsisShow: true,
        autoHidePrevious: true,
        autoHideNext: true,
        ajax: {
            beforeSend: function () {
                container.prev().html('Loading data...');
            }
        },
        callback: function (data, pagination) {
            $("#items").empty().append($.map(data, Event));
            var dataHtml = '<ul>';

            $.each(data, function (index, item) {
                dataHtml += '<li>' + item.title + '</li>';
            });

            dataHtml += '</ul>';
            $('#paged').prev().html(dataHtml);
        }
    });
};


$('.basicAutoComplete').autoComplete({
    resolverSettings: {
        url: 'api/v1/events/autocomplete'
    }
});

$("#inputsearch").on('change', function () {
    pagin($(this).val());

});

const wrapper = document.querySelector(".input-wrapper"),
    textInput = document.querySelector("input[type='search']");

textInput.addEventListener("keyup", event => {
    wrapper.setAttribute("data-text", event.target.value);
});

$('.selects').on('click', '.btn', function () {
    $(this).prop("aria-expanded", true);
});
