var https = require('https');
var querystring = require('querystring');
var AWS = require("aws-sdk");
var dynamo = new AWS.DynamoDB.DocumentClient();
var TABLENAME = "oauth_tokens";
var KEYNAME = "google";

refreshToken = function (parameters, success, fail) {
    console.log("clientId=" + parameters.clientId);
    console.log("clientSecret=" + parameters.clientSecret);
    console.log("refreshToken=" + parameters.refreshToken);

    var options = {
        hostname: "accounts.google.com",
        port: 443,
        path: "/o/oauth2/token",
        method: "POST",
        headers: { "content-type": "application/x-www-form-urlencoded" }
    };

    var req = https.request(options, function (res) {
        res.setEncoding("utf8");
        res.on("data", function (body) {
            if (res.statusCode === 200) {
                var json = JSON.parse(body);
                success(json.access_token);
            } else {
                console.log("statusCode=" + res.statusCode.toString());
                console.error(res.error);
                fail(res);
            }
        });
    });

    req.on("error", function (err) {
        console.error(err);
        fail(err);
    });

    req.write("client_id=" + parameters.clientId);
    req.write("&");
    req.write("client_secret=" + parameters.clientSecret);
    req.write("&");
    req.write("refresh_token=" + parameters.refreshToken);
    req.write("&");
    req.write("grant_type=refresh_token");
    req.end();
};

exports.handler = function (event, context) {
    var condition = {
        TableName: TABLENAME,
        KeyConditionExpression: "#k = :str",
        ExpressionAttributeValues: { ":str": KEYNAME },
        ExpressionAttributeNames: { "#k": "key" }
    };
    dynamo.query(
        condition,
        function (err, data) {
            if (err) {
                console.error("[ERROR] dynamodb access failed");
                console.error(err.toString());
                context.fail({ result: "error", reason: "dynamodb access failed" });
                return;
            }

            var parameters = data.Items[0];
            var update = function (newToken) {
                parameters.token = newToken;
                parameters.timestamp = (new Date()).toString();
                dynamo.put({ TableName: TABLENAME, Item: parameters }, function (err, data) {
                    if (err) {
                        console.error(err);
                        context.fail({ result: "error", message: "failed to update dynamodb", details: err });
                    }
                    else { context.succeed(JSON.stringify({ result: "ok" })); }
                });
            };
            refreshToken(parameters, update, context.fail);
        }
    );
};
