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
	public partial class ViewFileField : ViewControl
	{
		public dynamic upload_handler = XVar.Pack(null);
		protected static bool skipViewFileFieldCtor = false;
		public ViewFileField(dynamic _param_field, dynamic _param_container, dynamic _param_pageobject)
			:base((XVar)_param_field, (XVar)_param_container, (XVar)_param_pageobject)
		{
			if(skipViewFileFieldCtor)
			{
				skipViewFileFieldCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic container = XVar.Clone(_param_container);
			dynamic pageobject = XVar.Clone(_param_pageobject);
			#endregion

			dynamic pageObject = null;
			initUploadHandler();
		}
		public override XVar getTextValue(dynamic data)
		{
			dynamic fileNames = XVar.Array(), filedIsUrl = null, filesData = XVar.Array();
			if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(data[this.field])))))
			{
				return "";
			}
			filedIsUrl = XVar.Clone(this.container.pSet.isVideoUrlField((XVar)(this.field)));
			if(XVar.Pack(filedIsUrl))
			{
				return data[this.field];
			}
			fileNames = XVar.Clone(XVar.Array());
			filesData = XVar.Clone(getFilesArray((XVar)(data[this.field])));
			foreach (KeyValuePair<XVar, dynamic> file in filesData.GetEnumerator())
			{
				fileNames.InitAndSetArrayItem(file.Value["usrName"], null);
			}
			return MVCFunctions.implode(new XVar(", "), (XVar)(fileNames));
		}
		public virtual XVar initUploadHandler()
		{
			if(XVar.Pack(this.upload_handler == null))
			{
				this.upload_handler = XVar.Clone(new UploadHandler((XVar)(CommonFunctions.getOptionsForMultiUpload((XVar)(this.container.pSet), (XVar)(this.field)))));
				if(XVar.Pack(!(XVar)(this.pageObject == null)))
				{
					this.upload_handler.pSet = XVar.Clone(this.pageObject.pSetEdit);
				}
				else
				{
					this.upload_handler.pSet = XVar.Clone(this.container.pSet);
				}
				this.upload_handler.field = XVar.Clone(this.field);
				this.upload_handler.table = XVar.Clone(this.container.pSet._table);
				this.upload_handler.pageType = XVar.Clone(this.container.pageType);
			}
			return null;
		}
		public virtual XVar getFilesArray(dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			#endregion

			dynamic filesArray = null;
			filesArray = XVar.Clone(MVCFunctions.my_json_decode((XVar)(value)));
			if((XVar)(!(XVar)(MVCFunctions.is_array((XVar)(filesArray))))  || (XVar)(MVCFunctions.count(filesArray) == 0))
			{
				if(value == XVar.Pack(""))
				{
					filesArray = XVar.Clone(XVar.Array());
				}
				else
				{
					dynamic uploadedFile = null;
					uploadedFile = XVar.Clone(this.upload_handler.get_file_object((XVar)(value)));
					if(XVar.Pack(uploadedFile == null))
					{
						filesArray = XVar.Clone(XVar.Array());
					}
					else
					{
						filesArray = XVar.Clone(new XVar(0, MVCFunctions.my_json_decode((XVar)(MVCFunctions.my_json_encode((XVar)(uploadedFile))))));
					}
				}
			}
			return filesArray;
		}
	}
}
