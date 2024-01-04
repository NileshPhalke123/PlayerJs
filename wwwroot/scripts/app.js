"use strict";
;
var BitPlayerJS = /** @class */ (function () {
    function BitPlayerJS() {
    }
    BitPlayerJS.init = function (dotnetObj, id, file) {
        BitPlayerJS._DotnetObj = dotnetObj;
        BitPlayerJS._Player = new Playerjs({ id: id, file: file });
        window.PlayerjsEvents = function (event, id, data) {
            var _a;
            console.log(event, id, data);
            (_a = BitPlayerJS._DotnetObj) === null || _a === void 0 ? void 0 : _a.invokeMethodAsync('PlayerjsEvents', event, id, data);
        };
    };
    BitPlayerJS.dispose = function () {
        var _a;
        (_a = BitPlayerJS._DotnetObj) === null || _a === void 0 ? void 0 : _a.dispose();
        BitPlayerJS._DotnetObj = undefined;
    };
    BitPlayerJS._Player = undefined;
    BitPlayerJS._DotnetObj = undefined;
    return BitPlayerJS;
}());
