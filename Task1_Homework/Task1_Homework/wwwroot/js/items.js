export const Event = function (item) {
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
};

export const createSelectorItem = function (item) {
    return `<option value="${item.id}">${item.name}</option>`;
};

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
};

function getDate(date) {
    return moment(date).format("dddd mmmm dS, yyyy");
};

function getTime(date) {
    return moment(date).format("HH:MM");
};

export default Event;