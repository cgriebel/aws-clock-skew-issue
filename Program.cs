using Amazon;
using Amazon.EC2;
using Amazon.Runtime;


AWSConfigs.LoggingConfig.LogTo = LoggingOptions.Console;
AWSConfigs.LoggingConfig.LogResponses = ResponseLoggingOption.OnError;

var credentials = new BasicAWSCredentials("access-key", "secret-key");

var config = new AmazonEC2Config()
{
    RegionEndpoint = RegionEndpoint.APSoutheast5,

    // 10 isn't required, but a higher retry count makes the performance impact more obvious
    MaxErrorRetry = 10,
};

try
{
    // We need a legitimate failure unrelated to clock skew to demonstrate the behavior, in our case
    // we used a region that was not enabled for the account so that an "AuthFailure" error code is returned.
    var client = new AmazonEC2Client(credentials, config);
    var response = await client.DescribeAccountAttributesAsync();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
