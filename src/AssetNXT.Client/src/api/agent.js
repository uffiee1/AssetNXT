"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var axios_1 = require("axios");
// Url needs to be changed when hosted in the cloud.
axios_1.default.defaults.baseURL = "https://localhost:5001/api";
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
var Geometric = {
    routes: function () { return requests.get("/routes"); }
};
// eslint-disable-next-line import/no-anonymous-default-export
exports.default = {
    Geometric: Geometric
};
//# sourceMappingURL=agent.js.map