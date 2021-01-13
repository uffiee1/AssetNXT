"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var axios_1 = require("axios");
// Url needs to be changed when hosted in the cloud.
var baseUrl = "http://assetnxt.australiaeast.azurecontainer.io/";
axios_1.default.defaults.baseURL = baseUrl + "/api";
var responseBody = function (response) { return response.data; };
var requests = {
    get: function (url) { return axios_1.default.get(url).then(responseBody); },
    post: function (url, body) {
        return axios_1.default.post(url, body).then(responseBody);
    },
    put: function (url, body) {
        return axios_1.default.put(url, body).then(responseBody);
    },
    delete: function (url) {
        return axios_1.default.delete(url).then(responseBody);
    },
};
var Stations = {
    getStationsByDeviceId: function (id) { return requests.get("/stations/all/" + id); },
    getStations: function () { return requests.get("/stations"); },
};
var Notification = {};
var Routes = {
    getRoutes: function () { return requests.get("/routes"); },
    applyRoute: function (id, route) { return requests.put("/routes/" + id, route); },
    createRoute: function (route) { return requests.put("/routes", route); },
    deleteRoute: function (id) { return requests.delete("/routes/" + id); },
    editRoute: function (id, route) { return requests.put("/constraints/" + id, route); },
};
var Telemetric = {
    getConstraints: function () { return requests.get("/constraints"); },
    applyConstraints: function (sla) { return requests.put("/constraints", sla); },
    createConstraints: function (sla) { return requests.post("/constraints", sla); },
    deleteConstraints: function (id) { return requests.delete("/constraints/" + id); },
    editConstraints: function (id, sla) { return requests.put("/constraints/" + id, sla); }
};
// eslint-disable-next-line import/no-anonymous-default-export
exports.default = {
    Notification: Notification,
    Stations: Stations,
    Routes: Routes,
    Telemetric: Telemetric
};
//# sourceMappingURL=agent.js.map