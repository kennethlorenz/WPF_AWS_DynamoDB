using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS_DynamoDB
{
    public class DDBOperation
    {
        public AmazonDynamoDBClient ddbClient;
        BasicAWSCredentials credentials;
        string tableName = "Lab2UserTable";
        //Table userTable;
        public bool userExists;

        //constructor
        public DDBOperation (/*string tableName*/)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json");

            var accessKeyId = builder.Build().GetSection("AWSCredentials").GetSection("AccesskeyID").Value;
            var secretKey = builder.Build().GetSection("AWSCredentials").GetSection("Secretaccesskey").Value;

            credentials = new BasicAWSCredentials(accessKeyId, secretKey);
            ddbClient = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.USEast1);
            //userTable = Table.LoadTable(ddbclient, tableName, DynamoDBEntryConversion.V2);
        }


        //creating table method for Lab2UserTable
        public async Task CreateTable()
        {
            //instantiate a create table request object
            CreateTableRequest request = new CreateTableRequest
            {
                TableName = tableName,
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    //2 attributes, email and password
                    new AttributeDefinition
                    {
                        AttributeName = "Email",
                        AttributeType = "S"
                    },
                    new AttributeDefinition
                    {
                        AttributeName = "Password",
                        AttributeType = "S"
                    }
                },

                KeySchema = new List<KeySchemaElement>
                {
                    //make email as partition key
                    new KeySchemaElement
                    {
                        AttributeName = "Email",
                        KeyType = "HASH"
                    },
                    //make password as sort key
                    new KeySchemaElement
                    {
                        AttributeName = "Password",
                        KeyType = "RANGE"
                    }
                },

                BillingMode = BillingMode.PROVISIONED,
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 2,
                    WriteCapacityUnits = 1
                }
                

            };

            try
            {
                var response = await ddbClient.CreateTableAsync(request);
                if(response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    Debug.WriteLine($"{tableName} table has been successfully created");
                }
            }

            catch(InternalServerErrorException iee)
            {
                Debug.WriteLine($"An error occured on the server side: {iee.Message}");
            }
            catch(LimitExceededException lee)
            {
                Debug.WriteLine($"You're creating a table with one or more indexes: {lee.Message}");
            }

            catch(ResourceInUseException ree)
            {
                Debug.WriteLine($"{tableName} already exists. :{ree.Message}");
            }
        }
        public async Task InsertLoginCredentials()
        {
            BatchWriteItemRequest batchRequest = new BatchWriteItemRequest
            {
                RequestItems = new Dictionary<string, List<WriteRequest>>
                {
                    {
                        tableName, new List<WriteRequest>
                        {
                            new WriteRequest
                            {
                                PutRequest = new PutRequest
                                {
                                    Item = new Dictionary<string, AttributeValue>
                                    {
                                        { "Email", new AttributeValue{ S = "kg@yahoo.com"} },
                                        { "Password", new AttributeValue {S = "password"} }
                                    }
                                }
                            },

                            new WriteRequest
                            {
                                PutRequest = new PutRequest
                                {
                                    Item = new Dictionary<string, AttributeValue>
                                    {
                                        { "Email", new AttributeValue{ S = "kz@yahoo.com"} },
                                        { "Password", new AttributeValue {S = "password"} }
                                    }
                                }
                            },

                             new WriteRequest
                            {
                                PutRequest = new PutRequest
                                {
                                    Item = new Dictionary<string, AttributeValue>
                                    {
                                        { "Email", new AttributeValue{ S = "kl@yahoo.com"} },
                                        { "Password", new AttributeValue {S = "password"} }
                                    }
                                }
                            }
                        }
                    }
                },
            };

            //PutItemRequest request = new PutItemRequest
            //{
            //    //declare tablename (Lab2UserTable) to Tablename property of PutItemRequest
            //    TableName = tableName,
            //    Item = new Dictionary<string, AttributeValue>
            //    {
            //        { "Email", new AttributeValue{ S = "kg@yahoo.com"} },
            //        { "Password", new AttributeValue {S = "password"} },
            //    },
            //};

            try
            {
                var response = await ddbClient.BatchWriteItemAsync(batchRequest);
                if(response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    Debug.WriteLine($"3 Login credentials has been added to table {tableName}");
                }
            }

            catch(InternalServerErrorException iee)
            {
                Debug.WriteLine($"An error occured on the server side: {iee.Message}");
            }

            catch(ResourceNotFoundException rnfe)
            {
                Debug.WriteLine($"The operation tried to access a nonexistent table or index: {rnfe.Message}");
            }

            catch(DuplicateItemException die)
            {
                Debug.WriteLine($"Login credentials already exists: {die.Message}");
            }

        }
        public async Task GetUser(string email, string password)
        {
            GetItemRequest request = new GetItemRequest()
            {
                TableName = tableName,
                //primary key - should include partition and sort key
                Key = new Dictionary<string, AttributeValue>
                {
                    {"Email", new AttributeValue{S = email} },
                    {"Password", new AttributeValue{S = password} }
                }
            };

            try
            {
                var response = await ddbClient.GetItemAsync(request);
                if(response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    if(response.Item.Count > 0)
                    {
                        Debug.WriteLine($"User {email} successfully logged in!");
                        userExists = true;
                    }
                }
            }

            catch(InternalServerErrorException iee)
            {
                Debug.Write($"An error occured on the server side: {iee.Message}");
            }
            catch (ResourceNotFoundException rnfe)
            {
                Debug.Write($"User doesnt exist: {rnfe.Message}");
            }
        }
    }
}
