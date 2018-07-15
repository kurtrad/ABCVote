using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using runnerDotNet;
namespace runnerDotNet
{
	public partial class RunnerCipherer : XClass
	{
		public dynamic key = XVar.Pack("");
		public dynamic alg = XVar.Pack("");
		public dynamic mode = XVar.Pack("");
		protected dynamic strTableName = XVar.Pack("");
		protected dynamic ESFunctions = XVar.Pack(null);
		protected ProjectSettings pSet = null;
		protected dynamic encryptedFields = XVar.Array();
		protected dynamic connection;
		public RunnerCipherer(dynamic _param_strTableName, dynamic _param_pSet_packed = null)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region default values
			if(_param_pSet as Object == null) _param_pSet = null;
			#endregion

			#region pass-by-value parameters
			dynamic strTableName = XVar.Clone(_param_strTableName);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			#endregion

			this.strTableName = XVar.Clone(strTableName);
			setConnection();
			this.key = XVar.Clone(this.connection._encryptInfo["key"]);
			this.alg = XVar.Clone(this.connection._encryptInfo["alg"]);
			this.mode = XVar.Clone(this.connection._encryptInfo["mode"]);
			if(pSet != null)
			{
				this.pSet = XVar.UnPackProjectSettings(pSet);
			}
			else
			{
				this.pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(strTableName)));
			}
		}
		protected virtual XVar setConnection()
		{
			if(this.strTableName != Constants.NOT_TABLE_BASED_TNAME)
			{
				this.connection = XVar.Clone(GlobalVars.cman.byTable((XVar)(this.strTableName)));
			}
			else
			{
				this.connection = XVar.Clone(CommonFunctions.getDefaultConnection());
			}
			if((XVar)((XVar)(this.connection.dbType == Constants.nDATABASE_MSSQLServer)  && (XVar)(this.connection._encryptInfo["alg"] == Constants.ENCRYPTION_ALG_AES))  && (XVar)(this.connection._encryptInfo["mode"] == Constants.ENCRYPTION_DB))
			{
				dynamic symmetricSql = null;
				symmetricSql = XVar.Clone(MVCFunctions.mysprintf(new XVar("OPEN SYMMETRIC KEY %s DECRYPTION BY CERTIFICATE %s;"), (XVar)(new XVar(0, this.connection._encryptInfo["slqserverkey"], 1, this.connection._encryptInfo["slqservercert"]))));
				this.connection.setInitializingSQL((XVar)(symmetricSql));
			}
			return null;
		}
		public virtual XVar isEncryptionByPHPEnabled()
		{
			return this.connection.isEncryptionByPHPEnabled();
		}
		public virtual XVar DecryptFetchedArray(dynamic _param_fetchedArray)
		{
			#region pass-by-value parameters
			dynamic fetchedArray = XVar.Clone(_param_fetchedArray);
			#endregion

			dynamic result = XVar.Array();
			result = XVar.Clone(XVar.Array());
			if(XVar.Pack(fetchedArray))
			{
				if((XVar)(!(XVar)(this.pSet.hasEncryptedFields()))  || (XVar)(!(XVar)(this.connection.isEncryptionByPHPEnabled())))
				{
					return fetchedArray;
				}
				foreach (KeyValuePair<XVar, dynamic> fieldValue in fetchedArray.GetEnumerator())
				{
					result.InitAndSetArrayItem(DecryptField((XVar)(fieldValue.Key), (XVar)(fieldValue.Value)), fieldValue.Key);
				}
			}
			return result;
		}
		public virtual XVar isFieldEncrypted(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			dynamic table = null;
			table = XVar.Clone(this.strTableName);
			if((XVar)(this.encryptedFields.KeyExists(table))  && (XVar)(this.encryptedFields[table].KeyExists(field)))
			{
				return this.encryptedFields[table][field];
			}
			if(XVar.Pack(!(XVar)(this.encryptedFields.KeyExists(table))))
			{
				this.encryptedFields.InitAndSetArrayItem(XVar.Array(), table);
			}
			this.encryptedFields.InitAndSetArrayItem(this.pSet.isFieldEncrypted((XVar)(field)), table, field);
			return this.encryptedFields[table][field];
		}
		public virtual XVar isFieldPHPEncrypted(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return (XVar)(this.connection.isEncryptionByPHPEnabled())  && (XVar)(isFieldEncrypted((XVar)(field)));
		}
		public virtual XVar MakeDBValue(dynamic _param_field, dynamic _param_value, dynamic _param_controltype = null, dynamic _param_phpEncryptionOnly = null)
		{
			#region default values
			if(_param_controltype as Object == null) _param_controltype = new XVar("");
			if(_param_phpEncryptionOnly as Object == null) _param_phpEncryptionOnly = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic value = XVar.Clone(_param_value);
			dynamic controltype = XVar.Clone(_param_controltype);
			dynamic phpEncryptionOnly = XVar.Clone(_param_phpEncryptionOnly);
			#endregion

			dynamic ret = null;
			ret = XVar.Clone(CommonFunctions.prepare_for_db((XVar)(field), (XVar)(value), (XVar)(controltype), new XVar(""), (XVar)(this.strTableName)));
			if(XVar.Equals(XVar.Pack(ret), XVar.Pack(false)))
			{
				return ret;
			}
			ret = XVar.Clone(CommonFunctions.add_db_quotes((XVar)(field), (XVar)(EncryptField((XVar)(field), (XVar)(ret))), (XVar)(this.strTableName)));
			if(XVar.Pack(phpEncryptionOnly))
			{
				return ret;
			}
			return EncryptValueByDB((XVar)(field), (XVar)(ret));
		}
		public virtual XVar AddDBQuotes(dynamic _param_field, dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic value = XVar.Clone(_param_value);
			#endregion

			return EncryptValueByDB((XVar)(field), (XVar)(CommonFunctions.add_db_quotes((XVar)(field), (XVar)(EncryptField((XVar)(field), (XVar)(value))), (XVar)(this.strTableName))));
		}
		public virtual XVar GetLikeClause(dynamic _param_field, dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic value = XVar.Clone(_param_value);
			#endregion

			dynamic searchPattern = null;
			if((XVar)(this.connection.isEncryptionByPHPEnabled())  && (XVar)(isFieldEncrypted((XVar)(field))))
			{
				return MVCFunctions.Concat("=", this.connection.prepareString((XVar)(EncryptField((XVar)(field), (XVar)(value)))));
			}
			if(XVar.Pack(GlobalVars.ajaxSearchStartsWith))
			{
				searchPattern = XVar.Clone(MVCFunctions.Concat(value, "%"));
			}
			else
			{
				searchPattern = XVar.Clone(MVCFunctions.Concat("%", value, "%"));
			}
			return MVCFunctions.Concat(" LIKE ", this.connection.prepareString((XVar)(searchPattern)));
		}
		public virtual XVar GetLookupFieldName(dynamic _param_field, dynamic _param_fieldForCheck, dynamic _param_alias = null, dynamic _param_addAs = null)
		{
			#region default values
			if(_param_alias as Object == null) _param_alias = new XVar();
			if(_param_addAs as Object == null) _param_addAs = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic fieldForCheck = XVar.Clone(_param_fieldForCheck);
			dynamic alias = XVar.Clone(_param_alias);
			dynamic addAs = XVar.Clone(_param_addAs);
			#endregion

			if((XVar)(this.connection.isEncryptionByPHPEnabled())  || (XVar)(!(XVar)(isFieldEncrypted((XVar)(fieldForCheck)))))
			{
				return field;
			}
			return GetEncryptedFieldName((XVar)(field), (XVar)(alias), (XVar)(addAs));
		}
		public virtual XVar GetFieldName(dynamic _param_field, dynamic _param_alias = null, dynamic _param_addAs = null)
		{
			#region default values
			if(_param_alias as Object == null) _param_alias = new XVar();
			if(_param_addAs as Object == null) _param_addAs = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic alias = XVar.Clone(_param_alias);
			dynamic addAs = XVar.Clone(_param_addAs);
			#endregion

			if((XVar)(this.connection.isEncryptionByPHPEnabled())  || (XVar)(!(XVar)(isFieldEncrypted((XVar)((XVar.Pack(alias != null) ? XVar.Pack(alias) : XVar.Pack(field)))))))
			{
				return field;
			}
			return GetEncryptedFieldName((XVar)(field), (XVar)(alias), (XVar)(addAs));
		}
		public virtual XVar GetEncryptedFieldName(dynamic _param_field, dynamic _param_alias = null, dynamic _param_addAs = null)
		{
			#region default values
			if(_param_alias as Object == null) _param_alias = new XVar();
			if(_param_addAs as Object == null) _param_addAs = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic alias = XVar.Clone(_param_alias);
			dynamic addAs = XVar.Clone(_param_addAs);
			#endregion

			dynamic result = null;
			result = new XVar("");
			if(this.connection._encryptInfo["alg"] == Constants.ENCRYPTION_ALG_DES)
			{
				if(this.connection.dbType == Constants.nDATABASE_Oracle)
				{
					result = new XVar("utl_raw.cast_to_varchar2(DBMS_CRYPTO.DECRYPT(utl_raw.cast_to_raw(%s), 4353, utl_raw.cast_to_raw('%s')))");
				}
				else
				{
					if(this.connection.dbType == Constants.nDATABASE_MSSQLServer)
					{
						result = new XVar("CAST(DecryptByPassPhrase(N'%s', %s) as nvarchar)");
					}
					else
					{
						if(this.connection.dbType == Constants.nDATABASE_MySQL)
						{
							result = new XVar("cast(DES_DECRYPT(unhex(%s), '%s') as char)");
						}
						else
						{
							if(this.connection.dbType == Constants.nDATABASE_PostgreSQL)
							{
								result = new XVar("pgp_sym_decrypt(CAST(%s as bytea), '%s')");
							}
						}
					}
				}
			}
			else
			{
				if(this.connection._encryptInfo["alg"] == Constants.ENCRYPTION_ALG_AES)
				{
					if(this.connection.dbType == Constants.nDATABASE_Oracle)
					{
						result = new XVar("utl_raw.cast_to_varchar2(DBMS_CRYPTO.DECRYPT(utl_raw.cast_to_raw(%s), 4358, utl_raw.cast_to_raw('%s')))");
						this.key = XVar.Clone(MVCFunctions.substr((XVar)(this.key), new XVar(0), new XVar(16)));
					}
					else
					{
						if(this.connection.dbType == Constants.nDATABASE_MSSQLServer)
						{
							result = new XVar("CAST(DecryptByKey(%s) as nvarchar)");
							this.key = XVar.Clone(field);
						}
						else
						{
							if(this.connection.dbType == Constants.nDATABASE_MySQL)
							{
								result = new XVar("cast(AES_DECRYPT(unhex(%s), '%s') as char)");
							}
							else
							{
								if(this.connection.dbType == Constants.nDATABASE_PostgreSQL)
								{
									result = new XVar("pgp_sym_decrypt(CAST(%s as bytea), '%s', 'cipher-algo=aes128')");
								}
							}
						}
					}
				}
			}
			if(result == XVar.Pack(""))
			{
				return field;
			}
			if(this.connection.dbType == Constants.nDATABASE_MSSQLServer)
			{
				result = XVar.Clone(MVCFunctions.mysprintf((XVar)(result), (XVar)(new XVar(0, this.key, 1, field))));
			}
			else
			{
				result = XVar.Clone(MVCFunctions.mysprintf((XVar)(result), (XVar)(new XVar(0, field, 1, this.key))));
			}
			return (XVar.Pack(addAs) ? XVar.Pack(MVCFunctions.Concat(result, " as ", this.connection.addFieldWrappers((XVar)((XVar.Pack(alias != null) ? XVar.Pack(alias) : XVar.Pack(field)))))) : XVar.Pack(result));
		}
		public virtual XVar EncryptValueByDB(dynamic _param_field, dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic value = XVar.Clone(_param_value);
			#endregion

			dynamic result = null;
			if((XVar)(!(XVar)(isFieldEncrypted((XVar)(field))))  || (XVar)(this.connection.isEncryptionByPHPEnabled()))
			{
				return value;
			}
			result = new XVar("");
			if(this.connection._encryptInfo["alg"] == Constants.ENCRYPTION_ALG_DES)
			{
				if(this.connection.dbType == Constants.nDATABASE_Oracle)
				{
					result = new XVar("utl_raw.cast_to_varchar2(DBMS_CRYPTO.ENCRYPT(utl_raw.cast_to_raw(%s), 4353, utl_raw.cast_to_raw('%s')))");
				}
				else
				{
					if(this.connection.dbType == Constants.nDATABASE_MSSQLServer)
					{
						result = new XVar("EncryptByPassPhrase(N'%s', %s)");
					}
					else
					{
						if(this.connection.dbType == Constants.nDATABASE_MySQL)
						{
							result = new XVar("hex(DES_ENCRYPT(%s, '%s'))");
						}
						else
						{
							if(this.connection.dbType == Constants.nDATABASE_PostgreSQL)
							{
								result = new XVar("pgp_sym_encrypt(%s, '%s')");
							}
						}
					}
				}
			}
			else
			{
				if(this.connection._encryptInfo["alg"] == Constants.ENCRYPTION_ALG_AES)
				{
					if(this.connection.dbType == Constants.nDATABASE_Oracle)
					{
						result = new XVar("utl_raw.cast_to_varchar2(DBMS_CRYPTO.ENCRYPT(utl_raw.cast_to_raw(%s), 4358, utl_raw.cast_to_raw('%s')))");
						this.key = XVar.Clone(MVCFunctions.substr((XVar)(this.key), new XVar(0), new XVar(16)));
					}
					else
					{
						if(this.connection.dbType == Constants.nDATABASE_MSSQLServer)
						{
							result = new XVar("EncryptByKey(Key_GUID('%s'), %s)");
							this.key = XVar.Clone(this.connection._encryptInfo["slqserverkey"]);
						}
						else
						{
							if(this.connection.dbType == Constants.nDATABASE_MySQL)
							{
								result = new XVar("hex(AES_ENCRYPT(%s, '%s'))");
							}
							else
							{
								if(this.connection.dbType == Constants.nDATABASE_PostgreSQL)
								{
									result = new XVar("pgp_sym_encrypt(%s, '%s', 'cipher-algo=aes128')");
								}
							}
						}
					}
				}
			}
			if(result != XVar.Pack(""))
			{
				if(this.connection.dbType == Constants.nDATABASE_MSSQLServer)
				{
					result = XVar.Clone(MVCFunctions.mysprintf((XVar)(result), (XVar)(new XVar(0, this.key, 1, value))));
				}
				else
				{
					result = XVar.Clone(MVCFunctions.mysprintf((XVar)(result), (XVar)(new XVar(0, value, 1, this.key))));
				}
			}
			else
			{
				result = XVar.Clone(value);
			}
			return result;
		}
		public virtual XVar EncryptField(dynamic _param_field, dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic value = XVar.Clone(_param_value);
			#endregion

			if((XVar)(isFieldEncrypted((XVar)(field)))  && (XVar)(this.connection.isEncryptionByPHPEnabled()))
			{
				if(XVar.Pack(this.ESFunctions == null))
				{
					this.ESFunctions = XVar.Clone(getESFunctions());
				}
				return this.ESFunctions.Encrypt((XVar)(value));
			}
			return value;
		}
		public virtual XVar DecryptField(dynamic _param_field, dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic value = XVar.Clone(_param_value);
			#endregion

			if((XVar)(isFieldEncrypted((XVar)(field)))  && (XVar)(this.connection.isEncryptionByPHPEnabled()))
			{
				if(XVar.Pack(this.ESFunctions == null))
				{
					this.ESFunctions = XVar.Clone(getESFunctions());
				}
				return this.ESFunctions.Decrypt((XVar)(value));
			}
			return value;
		}
		public virtual XVar getESFunctions()
		{
			if(this.connection._encryptInfo["alg"] == Constants.ENCRYPTION_ALG_DES)
			{
				return new RunnerCiphererDES((XVar)(this.key));
			}
			if(this.connection._encryptInfo["alg"] == Constants.ENCRYPTION_ALG_AES)
			{
				return new RunnerCiphererAES((XVar)(this.key));
			}
			return null;
		}
		public static XVar getForLogin(dynamic _param_loginSet = null)
		{
			#region default values
			if(_param_loginSet as Object == null) _param_loginSet = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic loginSet = XVar.Clone(_param_loginSet);
			#endregion

			return new RunnerCipherer(new XVar("dbo._ABCSecurity"), (XVar)(loginSet));
			return null;
		}
	}
}
