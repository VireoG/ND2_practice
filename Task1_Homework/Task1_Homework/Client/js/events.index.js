import "bootstrap";
import "bootstrap-autocomplete";
import "bootstrap-select";
import "paginationjs";

export const filt = {
    cities: [],
    venues: [],
    sortBy: 'Date',
    sortOrder: 'Ascending',
};

var search;

const selects = {
    cities: [],
    venues: []
};

const events = {
    event: []
};

function createItem(item) {
    return `<div class="block1ForEvent">
            <div id="modDialog" class="modal fade">
                <div id="dialogContent" class="modal-dialog"></div>
            </div>
                       <div id="template">
                            <div id="picture">
                                <img src="/img/${item.banner}" width="230px" height="350px" "/> 
                            </div>
                            <div class="content">
                                <div id="header">
                                     <h3 align="center">${item.name}</h3>
                                </div>
                                <div id="description">
                                    <p>${item.description}</p>
                                </div>
                                <div class="dateev">    
                                    <h6 class="h66">${getDate(item.date)} <br/> at ${getTime(item.date)}</h6> 
                                </div>
                                <div> 
                                     <a href="/Event/Buy/${item.id}" class="btnevents"><p class="pbtnevents">Buy Ticket</p></a>
                                </div>
                         </div>
                     </div>
                ${roleValidation(item)}`;
}

function createSelectItem(item) {
    return `<option value="${item.id}">${item.name}</option>`;
}

function getDate(date) {
    let dateFormat = require("dateformat");
    let dateform = new Date(date);
    return dateFormat(dateform, "dddd mmmm dS, yyyy");
}

function getTime(date) {
    let dateFormat = require("dateformat");
    let dateform = new Date(date);
    return dateFormat(dateform, "HH:MM");
}

function roleValidation(item) {
    if (role == "Administrator") {
        let stringforadmin = `<div class="divfbtn"> <div class="divfbtns"><a href="/Event/Edit/${item.id}" class="btnedit"><p>Edit</p></a></div>
               <div class="divfbtn"> <a href="/Event/Delete/${item.id}" class="btndelete compItem"><p>Delete</p></a> </div></div>
                </div>`;

        return stringforadmin;
    } else {
        let stringforothers = `</div>`;
        return stringforothers;
    }
}

$(document).ready(function getCities() {
    $.ajax({
        url: `/api/v1/city`,
        data: filt.cities,
        traditional: true,
        success: function (data) {
            selects.cities = data;
            $("#cities").empty().append($.map(data, createSelectItem));
            $("#cities").selectpicker('refresh');
        }
    });
});

$(document).ready(function () {
    pagin();
    $("#cities").on('change', function () {
        filt.cities = $(this).val();
        selects.cities = filt.cities;
        getVenues();
        pagin();    
    });
    $("#venues").on('change', function () {
        filt.venues = $(this).val();
        pagin();
    });
    $("#sortby").on('change', function () {
        filt.sortBy = $(this).val();
        pagin();
    });
    if (selects.cities == null) { $("#venues").prop("disabled", true); }
});

function getVenues() {
    $.ajax({
        url: `/api/v1/venue/getvenues`,
        data: selects,
        traditional: true,
        success: function (data) {
            selects.venues = data;         
            $("#venues").empty().append($.map(data, createSelectItem));
            if (selects.cities == null) {
                $("#venues").prop("disabled", true);
            } else {
                $("#venues").prop("disabled", false);
            }      
            $('#venues').selectpicker('refresh');
        }
    });
}

function pagin(search) {
    let num;
    $('#paged').pagination({
        dataSource: function (done) {
            $.ajax({
                url: `/api/v1/filters/pagedata`,
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
            $("#items").empty().append($.map(data, createItem));
            $(function () {
                $.ajaxSetup({cache: false});
                $('.compItem').click(function(e) {
                    e.preventDefault();
                    $.get(this.href, function(data) {
                        $('#dialogContent').html(data);
                        $('#modDialog').modal('show');
                    });
                });
            });

            var dataHtml = '<ul class="paginationbutt">';

            $.each(data, function (index, item) {
                dataHtml += '<li>' + item.title + '</li>';
            });

            dataHtml += '</ul>';
            $('#paged').prev().html(dataHtml);
        }
    });
}

$('.basicAutoComplete').autoComplete({
    resolverSettings: {
        url: 'api/v1/filters/autocomplete'
    }
});

$("#inputsearch").keyup(event => {
    if (event.key === '13') {
        search = $(this).val();
        pagin(search);
        event.preventDefault();
    }
});
