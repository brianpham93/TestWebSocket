using Neo4jClient;
using GSEPWebAPI.Helpers.Neo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSEPWebAPI.Helpers.Neo
{
    public class Neo4jHelper
    {
        private string ServerURL { get; set; }
        private static GraphClient _graphClient;

        public Neo4jHelper(string serverURL)
        {
            this.ServerURL = serverURL;
            _graphClient = new GraphClient(new Uri(ServerURL));
            _graphClient.Connect();
        }

        /// <summary>
        /// Create node to neo4j server
        /// </summary>
        /// <param name="mObject">Object which use to create node</param>
        /// <param name="label">Label of this node</param>
        public void CreateNode<T>(T mObject, string label)
        {
            string createString = "(n:" + label + " {mObject})";
            _graphClient.Cypher
                .Create(createString)
                .WithParam("mObject", mObject)
                .ExecuteWithoutResults();
        }

        /// <summary>
        /// Create node if it does not exist
        /// </summary>
        /// <param name="mObject">Object which use to create node</param>
        /// <param name="label">Label of this node</param>
        /// <param name="IDKey">KeyString(Ex: ID)</param>
        /// <param name="IDValue">KeyString value (Ex: SE02456)</param>                
        public void CreateNodeIfDoesNotExist<T>(T mObject, string label, KeyString mKey)
        {
            if (GetNode<T>(label, mKey) == null)
            {
                CreateNode(mObject, label);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="labelNode1"></param>
        /// <param name="mKeyNode1"></param>
        /// <param name="labelNode2"></param>
        /// <param name="mKeyNode2"></param>
        /// <param name="relationshipName"></param>
        /// <param name="relationshipType"></param>
        public void CreateRelationShip<T>(string labelNode1, KeyString mKeyNode1, string labelNode2, KeyString mKeyNode2, string relationshipName, T relationshipType)
        {
            string matchString1 = "(n1:" + labelNode1 + ")";
            string whereString1 = "n1." + mKeyNode1.KeyName + "='" + mKeyNode1.KeyValue + "'";
            string matchString2 = "(n2:" + labelNode2 + ")";
            string whereString2 = "n2." + mKeyNode2.KeyName + "='" + mKeyNode2.KeyValue + "'";
            if (relationshipType == null)
            {
                _graphClient.Cypher
                .Match(matchString1, matchString2)
                .Where(whereString1)
                .AndWhere(whereString2)
                .Create("n1-[:" + relationshipName + "]->n2")
                .ExecuteWithoutResults();
            }
            else
            {
                _graphClient.Cypher
                .Match(matchString1, matchString2)
                .Where(whereString1)
                .AndWhere(whereString2)
                .Create("n1-[:" + relationshipName + " {relationship}]->n2")
                .WithParam("relationship", relationshipType)
                .ExecuteWithoutResults();
            }
        }


        public void UpdateRelationShip<T>(string labelNode1, KeyString mKeyNode1, string labelNode2, KeyString mKeyNode2, string relationshipName, T relationshipType)
        {
            string matchString = "(n1:" + labelNode1 + ")-[r:" + relationshipName + "]->" + "(n2:" + labelNode2 + ")";
            string whereString = "n1." + mKeyNode1.KeyName + "='" + mKeyNode1.KeyValue + "' and " +
                                 "n2." + mKeyNode2.KeyName + "='" + mKeyNode2.KeyValue + "'";
            _graphClient.Cypher
                .Match(matchString)
                .Where(whereString)
                .Set("r = {update}")
                .WithParam("update", relationshipType).ExecuteWithoutResults();
        }

        public T GetRelationShip<T>(string labelNode1, KeyString mKeyNode1, string labelNode2, KeyString mKeyNode2, string relationshipName)
        {
            string matchString = "(n1:" + labelNode1 + ")-[r:" + relationshipName + "]->" + "(n2:" + labelNode2 + ")";
            string whereString = "n1." + mKeyNode1.KeyName + "='" + mKeyNode1.KeyValue + "' and " +
                                 "n2." + mKeyNode2.KeyName + "='" + mKeyNode2.KeyValue + "'";
            var result = _graphClient.Cypher
                .Match(matchString)
                .Where(whereString)
                .Return<T>("r").Results;
            if (result != null && result.Count() != 0)
            {
                return result.FirstOrDefault();
            }
            return default(T);
        }

        public void DeleteRelationShip(string labelNode1, KeyString mKeyNode1, string labelNode2, KeyString mKeyNode2, string relationshipName)
        {
            string matchString = "(n1:" + labelNode1 + ")-[r:" + relationshipName + "]->" + "(n2:" + labelNode2 + ")";
            string whereString = "n1." + mKeyNode1.KeyName + "='" + mKeyNode1.KeyValue + "' and " +
                                 "n2." + mKeyNode2.KeyName + "='" + mKeyNode2.KeyValue + "'";
            _graphClient.Cypher
            .Match(matchString)
            .Where(whereString)
            .Delete("r")
            .ExecuteWithoutResults();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mObject"></param>
        /// <param name="label"></param>
        /// <param name="mKey"></param>
        /// <returns></returns>
        public String UpdateNode<T>(T mObject, string label, KeyString mKey)
        {
            try
            {
                string matchString = "(n:" + label + ")";
                string whereString = "n." + mKey.KeyName + "='" + mKey.KeyValue + "'";
                _graphClient.Cypher.Match(matchString).Where(whereString).Set("n = {update}")
                            .WithParam("update", mObject).ExecuteWithoutResults();
                return "Update successfully";
            }
            catch (Exception e)
            {
                return "Update fail: " + e.Message;
            }
        }
        /// <summary>
        /// Get Specific node by ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="label"></param>
        /// <param name="IDKey"></param>
        /// <param name="IDValue"></param>
        /// <returns></returns>
        public T GetNode<T>(string label, KeyString mKey)
        {
            string matchString = "(n:" + label + ")";
            string whereString = "n." + mKey.KeyName + "='" + mKey.KeyValue + "'";
            var result = _graphClient.Cypher
                        .Match(matchString)
                        .Where(whereString)
                        .Return<T>("n").Results;
            if (result != null)
            {
                return result.FirstOrDefault();
            }
            return default(T);
        }
        /// <summary>
        /// Get nodes with more than one condition
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="label"></param>
        /// <param name="mKeys"></param>
        /// <returns></returns>
        public IEnumerable<T> GetNodes<T>(string label, KeyString[] mKeys, KeyInt[] mKeysInt)
        {
            string matchString = "(n:" + label + ")";
            string whereString = "";
            if (mKeys != null)
            {
                for (int i = 0; i < mKeys.Length - 1; i++)
                {
                    whereString = whereString + "n." + mKeys[i].KeyName + "='" + mKeys[i].KeyValue + "' and ";
                }
                whereString = whereString + "n." + mKeys[mKeys.Length - 1].KeyName + "='" + mKeys[mKeys.Length - 1].KeyValue + "'";
            }
            if (mKeysInt != null)
            {
                if (whereString.Equals(""))
                {
                    for (int i = 0; i < mKeysInt.Length - 1; i++)
                    {
                        whereString = whereString + "n." + mKeysInt[i].KeyName + "=" + mKeysInt[i].KeyValue + " and ";
                    }
                    whereString = whereString + "n." + mKeysInt[mKeysInt.Length - 1].KeyName + "=" + mKeysInt[mKeysInt.Length - 1].KeyValue + "";
                }
                else
                {
                    for (int i = 0; i < mKeysInt.Length; i++)
                    {
                        whereString = whereString + " and n." + mKeysInt[i].KeyName + "=" + mKeysInt[i].KeyValue;
                    }
                }
            }
            var result = _graphClient.Cypher
                        .Match(matchString)
                        .Where(whereString)
                        .Return<T>("n").Results;
            if (result != null)
            {
                return result;
            }
            return default(IEnumerable<T>);
        }


        public IEnumerable<T> GetNodes<T>(string label, KeyString[] mKeys, KeyInt[] mKeysInt, string relNodeLabel, KeyString keyRelNode, string relationshipName)
        {
            string matchString = "(n: " + label + ")-[r:" + relationshipName + "]->(m:" + relNodeLabel + ")";
            string whereString = "";
            if (mKeys != null)
            {
                for (int i = 0; i < mKeys.Length - 1; i++)
                {
                    whereString = whereString + "n." + mKeys[i].KeyName + "='" + mKeys[i].KeyValue + "' and ";
                }
                whereString = whereString + "n." + mKeys[mKeys.Length - 1].KeyName + "='" + mKeys[mKeys.Length - 1].KeyValue + "'";
            }
            if (mKeysInt != null)
            {
                if (whereString.Equals(""))
                {
                    for (int i = 0; i < mKeysInt.Length - 1; i++)
                    {
                        whereString = whereString + "n." + mKeysInt[i].KeyName + "=" + mKeysInt[i].KeyValue + " and ";
                    }
                    whereString = whereString + "n." + mKeysInt[mKeysInt.Length - 1].KeyName + "=" + mKeysInt[mKeysInt.Length - 1].KeyValue + "";
                }
                else
                {
                    for (int i = 0; i < mKeysInt.Length; i++)
                    {
                        whereString = whereString + " and n." + mKeysInt[i].KeyName + "=" + mKeysInt[i].KeyValue;
                    }
                }
            }
            whereString = whereString + " and m." + keyRelNode.KeyName + "='" + keyRelNode.KeyValue + "'";
            var result = _graphClient.Cypher
                        .Match(matchString)
                        .Where(whereString)
                        .Return<T>("n").Results;
            if (result != null)
            {
                return result;
            }
            return default(IEnumerable<T>);
        }

        /// <summary>
        /// Get all node by label
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="label"></param>
        /// <returns></returns>
        public IEnumerable<T> GetNodes<T>(string label)
        {
            string matchString = "(n:" + label + ")";
            var result = _graphClient.Cypher
                        .Match(matchString)
                        .Return<T>("n").Results;
            return result;
        }

        /// <summary>
        /// Get node which have Label 'label' and have 2 relationship
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="label"></param>
        /// <param name="labelNode1"></param>
        /// <param name="mKeyNode1"></param>
        /// <param name="relationshipName1"></param>
        /// <param name="labelNode2"></param>
        /// <param name="mKeyNode2"></param>
        /// <param name="relationshipName2"></param>
        /// <returns></returns>
        public IEnumerable<T> GetNodes<T>(string label, string labelNode1, KeyString mKeyNode1, string relationshipName1, string labelNode2, KeyString mKeyNode2, string relationshipName2)
        {
            string matchString = "(n1: " + labelNode1 + ")-[r1:" + relationshipName1 + "]->(n:" + label + "), "
                                + "(n2: " + labelNode2 + ")-[r2:" + relationshipName2 + "]->(n:" + label + ")";
            string whereString = "n1." + mKeyNode1.KeyName + "='" + mKeyNode1.KeyValue + "' and " +
                                 "n2." + mKeyNode2.KeyName + "='" + mKeyNode2.KeyValue + "'";
            var result = _graphClient.Cypher
                        .Match(matchString)
                        .Where(whereString)
                        .Return<T>("n").Results;
            return result;
        }

        /// <summary>
        /// Get node have Label 'label' and have relationship with node n (n:lableNode-[r]->m:label)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="label"></param>
        /// <param name="labelNode"></param>
        /// <param name="mKeyNode"></param>
        /// <param name="relationshipName"></param>
        /// <returns></returns>
        public IEnumerable<T> GetNodes<T>(string label, string labelNode, KeyString mKeyNode, string relationshipName)
        {
            string matchString = "(n1: " + labelNode + ")-[r:" + relationshipName + "]->(n:" + label + ")";
            string whereString = "n1." + mKeyNode.KeyName + "='" + mKeyNode.KeyValue + "'";
            var result = _graphClient.Cypher
                        .Match(matchString)
                        .Where(whereString)
                        .Return<T>("n").Results;
            return result;
        }

        /// <summary>
        /// Get node have Label 'label' and have relationship with node n (n:label-[r]->m:labelNode)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="labelNode"></param>
        /// <param name="mKeyNode"></param>
        /// <param name="label"></param>
        /// <param name="relationshipName"></param>
        /// <returns></returns>
        public IEnumerable<T> GetNodes<T>(string labelNode, KeyString mKeyNode, string label, string relationshipName)
        {
            string matchString = "(n: " + label + ")-[r:" + relationshipName + "]->(n1:" + labelNode + ")";
            string whereString = "n1." + mKeyNode.KeyName + "='" + mKeyNode.KeyValue + "'";
            var result = _graphClient.Cypher
                        .Match(matchString)
                        .Where(whereString)
                        .Return<T>("n").Results;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="label"></param>
        /// <param name="keyword"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public IEnumerable<T> SearchNodes<T>(string label, string keyword, string[] keyName)
        {
            string matchString = "(n:" + label + ")";
            string whereString = "";
            for (int i = 0; i < keyName.Length - 1; i++)
            {
                whereString = whereString + "n." + keyName[i] + "=~ '.*(?i)" + keyword + ".*' or ";
            }
            whereString = whereString + "n." + keyName[keyName.Length - 1] + "=~ '.*(?i)" + keyword + ".*'";
            var result = _graphClient.Cypher
                        .Match(matchString)
                        .Where(whereString)
                        .Return<T>("n").Results;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relationshipName"></param>
        /// <param name="labelNode1"></param>
        /// <param name="mKeyNode1"></param>
        /// <param name="labelNode2"></param>
        /// <param name="mKeyNode2"></param>
        /// <returns></returns>
        public bool IsRelate(string relationshipName, string labelNode1, KeyString mKeyNode1, string labelNode2, KeyString mKeyNode2)
        {
            string matchString = "(n:" + labelNode1 + ")-" + "[r:" + relationshipName + "]->" + "(m:" + labelNode2 + ")";
            string whereString = "n." + mKeyNode1.KeyName + "='" + mKeyNode1.KeyValue + "' and " + "m." + mKeyNode2.KeyName + "='" + mKeyNode2.KeyValue + "'";
            int result = _graphClient.Cypher
                        .Match(matchString)
                        .Where(whereString)
                        .Return<Object>("r").Results.Count();
            if (result == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool DeleteNode(string label, KeyString mKey)
        {
            string matchString = "(n:" + label + ")";
            string optionalMatchString = "(n)-[r]-()";
            string whereString = "n." + mKey.KeyName + "='" + mKey.KeyValue + "'";
            _graphClient.Cypher
                .Match(matchString)
                .OptionalMatch(optionalMatchString)
                .Where(whereString)
                .Delete("r").ExecuteWithoutResults();
            _graphClient.Cypher
                .Match(matchString)
                .Where(whereString)
                .Delete("n").ExecuteWithoutResults();
            return true;
        }

    }
}