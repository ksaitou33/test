var https = require('https');
var querystring = require('querystring');
var AWS = require("aws-sdk");
var dynamo = new AWS.DynamoDB.DocumentClient();
var TOKEN_TABLENAME = "oauth_tokens";
var TOKEN_KEYNAME = "google";
var SCHEDULE_TABLENAME = "calendar_events";
var SCHEDULE_KEYNAME = "vlpuud8e5ql62t3hi715jeko9k@group.calendar.google.com";
var jsonItems = "";

getSchedule = function (token, success, fail) {
    var path = "/calendar/v3/calendars/" + SCHEDULE_KEYNAME + "/events/"
        + "?maxResults=14"
        + "&orderBy=startTime"
        + "&singleEvents=true"
        + "&timeMin=" + (new Date()).toISOString();

    var options = {
        hostname: "www.googleapis.com",
        port: 443,
        path: path,
        method: "GET",
        headers: { "Authorization": "Bearer " + token }
    };

    var req = https.request(options, function (res) {
        res.setEncoding("utf8");
        res.on("data", function (body) {
            if (res.statusCode === 200) {
                success(body);
            } else {
                console.log("statusCode=" + res.statusCode.toString());
                console.error(res.statusMessage);
                fail(res.statusMessage);
            }
        });
    });

    req.on("error", function (err) {
        console.error(err);
        fail(err);
    });

    req.end();
};

exports.handler = function (event, context) {
    var condition = {
        TableName: TOKEN_TABLENAME,
        KeyConditionExpression: "#k = :str",
        ExpressionAttributeValues: { ":str": TOKEN_KEYNAME },
        ExpressionAttributeNames: { "#k": "key" }
    };
    dynamo.query(
        condition,
        function (err, data) {
            if (err) {
                console.error("[ERROR] dynamodb(token) access failed");
                console.error(err.toString());
                context.fail({ result: "error", reason: "dynamodb(token) access failed" });
            }

            var token = data.Items[0].token;

            var update = function (body) {
                jsonItems = jsonItems + body;
                try {
                    var json = JSON.parse(jsonItems);
                    condition = {
                        TableName: SCHEDULE_TABLENAME,
                        KeyConditionExpression: "#k = :str",
                        ExpressionAttributeValues: { ":str": SCHEDULE_KEYNAME },
                        ExpressionAttributeNames: { "#k": "calendarId" }
                    };

                    dynamo.query(
                        condition,
                        function (err, data) {
                            if (err) {
                                console.error("[ERROR] dynamodb(schedule) access failed");
                                console.error(err.toString());
                                context.fail({ result: "error", reason: "dynamodb(schedule) access failed" });
                            }

                            var parameters = data.Items[0];
                            parameters.items = json.items;
                            parameters.timestamp = (new Date()).toString();
                            dynamo.put({ TableName: SCHEDULE_TABLENAME, Item: parameters }, function (err, data) {
                                if (err) {
                                    console.error(err);
                                    context.fail({ result: "error", message: "failed to update dynamodb", details: err });
                                }
                                else {
                                    context.succeed(JSON.stringify({ result: "ok" }));
                                }
                            });
                        }
                    );
                } catch(e) {
                }
            };
            getSchedule(token, update, context.fail);
        }
    );
};
