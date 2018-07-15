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
	public partial class XTempl_Base : XClass
	{
		public dynamic xt_vars = XVar.Array();
		public dynamic xt_stack;
		public dynamic xt_events = XVar.Array();
		public dynamic template;
		public dynamic template_file;
		public dynamic charsets = XVar.Array();
		public dynamic testingFlag = XVar.Pack(false);
		public dynamic eventsObject;
		public dynamic hiddenBricks = XVar.Array();
		public dynamic preparedContainers = XVar.Array();
		public dynamic layout;
		public dynamic pageId = XVar.Pack(1);
		public dynamic cssFiles = XVar.Array();
		public virtual XVar getVar(dynamic _param_name)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			#endregion

			return MVCFunctions.xt_getvar(this, (XVar)(name));
		}
		public virtual XVar recTesting(dynamic arr)
		{
			foreach (KeyValuePair<XVar, dynamic> v in arr.GetEnumerator())
			{
				if(XVar.Pack(MVCFunctions.is_array((XVar)(v.Value))))
				{
					recTesting((XVar)(arr[v.Key]));
				}
				else
				{
					if(XVar.Pack(GlobalVars.testingLinks.KeyExists(v.Key)))
					{
						arr[v.Key] = MVCFunctions.Concat(arr[v.Key], " func=\"", GlobalVars.testingLinks[v.Key], "\"");
					}
				}
			}
			return null;
		}
		public virtual XVar Testing()
		{
			if(XVar.Pack(!(XVar)(this.testingFlag)))
			{
				return null;
			}
			recTesting((XVar)(this.xt_vars));
			return null;
		}
		public virtual XVar report_error(dynamic _param_message)
		{
			#region pass-by-value parameters
			dynamic message = XVar.Clone(_param_message);
			#endregion

			MVCFunctions.Echo(message);
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}
		protected virtual XVar assign_headers()
		{
			if(XVar.Pack(this.xt_vars.KeyExists("header")))
			{
				return null;
			}
			if(XVar.Pack(!(XVar)(mobileTemplateMode())))
			{
				assign(new XVar("header"), new XVar("header"));
				assign(new XVar("footer"), new XVar("footer"));
			}
			else
			{
				assign(new XVar("header"), new XVar("mheader"));
				assign(new XVar("footer"), new XVar("mfooter"));
			}
			return null;
		}
		public XTempl_Base(dynamic _param_hideAddedCharts = null)
		{
			#region default values
			if(_param_hideAddedCharts as Object == null) _param_hideAddedCharts = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic hideAddedCharts = XVar.Clone(_param_hideAddedCharts);
			#endregion

			dynamic html_attrs = null;
			this.xt_vars = XVar.Clone(XVar.Array());
			this.xt_stack = XVar.Clone(XVar.Array());
			this.xt_stack.InitAndSetArrayItem(this.xt_vars, null);
			assign_method(new XVar("event"), this, new XVar("xt_doevent"), (XVar)(XVar.Array()));
			assign_function(new XVar("label"), new XVar("xt_label"), (XVar)(XVar.Array()));
			assign_function(new XVar("tooltip"), new XVar("xt_tooltip"), (XVar)(XVar.Array()));
			assign_function(new XVar("custom"), new XVar("xt_custom"), (XVar)(XVar.Array()));
			assign_function(new XVar("caption"), new XVar("xt_caption"), (XVar)(XVar.Array()));
			assign_function(new XVar("pagetitlelabel"), new XVar("xt_pagetitlelabel"), (XVar)(XVar.Array()));
			assign_method(new XVar("mainmenu"), this, new XVar("xt_displaymainmenu"), (XVar)(XVar.Array()));
			assign_method(new XVar("menu"), this, new XVar("xt_displaymenu"), (XVar)(XVar.Array()));
			assign_function(new XVar("TabGroup"), new XVar("xt_displaytabs"), (XVar)(XVar.Array()));
			assign_function(new XVar("Section"), new XVar("xt_displaytabs"), (XVar)(XVar.Array()));
			assign_function(new XVar("Step"), new XVar("xt_displaytabs"), (XVar)(XVar.Array()));
			assign_function(new XVar("logo"), new XVar("printProjectLogo"), (XVar)(XVar.Array()));
			assign_function(new XVar("home_link"), new XVar("printHomeLink"), (XVar)(XVar.Array()));
			if(XVar.Pack(!(XVar)(hideAddedCharts)))
			{
			}
			GlobalVars.mlang_charsets = XVar.Clone(XVar.Array());
			GlobalVars.mlang_charsets["English"] = "Windows-1252";

			this.charsets = GlobalVars.mlang_charsets;
			html_attrs = new XVar("");
			if(XVar.Pack(CommonFunctions.isRTL()))
			{
				assign(new XVar("RTL_block"), new XVar(true));
				assign(new XVar("rtlCSS"), new XVar(true));
				html_attrs = MVCFunctions.Concat(html_attrs, "dir=\"RTL\" ");
			}
			else
			{
				assign(new XVar("LTR_block"), new XVar(true));
			}
			if(CommonFunctions.mlang_getcurrentlang() == "English")
			{
				html_attrs = MVCFunctions.Concat(html_attrs, "lang=\"en\"");
			}
			assign(new XVar("html_attrs"), (XVar)(html_attrs));
			assign(new XVar("menu_block"), new XVar(true));
		}
		public virtual XVar assign(dynamic _param_name, dynamic _param_val)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			dynamic val = XVar.Clone(_param_val);
			#endregion

			this.xt_vars.InitAndSetArrayItem(val, name);
			return null;
		}
		public virtual XVar assignbyref(dynamic _param_name, dynamic var_var)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			#endregion

			this.xt_vars.InitAndSetArrayItem(var_var, name);
			return null;
		}
		public virtual XVar bulk_assign(dynamic _param_arr)
		{
			#region pass-by-value parameters
			dynamic arr = XVar.Clone(_param_arr);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> value in arr.GetEnumerator())
			{
				this.xt_vars.InitAndSetArrayItem(value.Value, value.Key);
			}
			return null;
		}
		public virtual XVar enable_section(dynamic _param_name)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			#endregion

			if(XVar.Pack(!(XVar)(this.xt_vars.KeyExists(name))))
			{
				this.xt_vars.InitAndSetArrayItem(true, name);
			}
			else
			{
				if(this.xt_vars[name] == false)
				{
					this.xt_vars.InitAndSetArrayItem(true, name);
				}
			}
			return null;
		}
		public virtual XVar assign_section(dynamic _param_name, dynamic _param_begin, dynamic _param_end)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			dynamic begin = XVar.Clone(_param_begin);
			dynamic var_end = XVar.Clone(_param_end);
			#endregion

			dynamic arr = XVar.Array();
			arr = XVar.Clone(XVar.Array());
			arr.InitAndSetArrayItem(begin, "begin");
			arr.InitAndSetArrayItem(var_end, "end");
			this.xt_vars.InitAndSetArrayItem(arr, name);
			return null;
		}
		public virtual XVar assign_loopsection(dynamic _param_name, dynamic data)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			#endregion

			dynamic arr = XVar.Array();
			arr = XVar.Clone(XVar.Array());
			arr.InitAndSetArrayItem(data, "data");
			this.xt_vars.InitAndSetArrayItem(arr, name);
			return null;
		}
		public virtual XVar assign_array(dynamic _param_name, dynamic _param_innername, dynamic _param__arr)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			dynamic innername = XVar.Clone(_param_innername);
			dynamic _arr = XVar.Clone(_param__arr);
			#endregion

			dynamic arr = XVar.Array();
			arr = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> a in _arr.GetEnumerator())
			{
				arr.InitAndSetArrayItem(new XVar(innername, a.Value), null);
			}
			this.xt_vars.InitAndSetArrayItem(new XVar("data", arr), name);
			return null;
		}
		public virtual XVar assign_loopsection_byValue(dynamic _param_name, dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			dynamic data = XVar.Clone(_param_data);
			#endregion

			dynamic arr = XVar.Array();
			arr = XVar.Clone(XVar.Array());
			arr.InitAndSetArrayItem(data, "data");
			this.xt_vars.InitAndSetArrayItem(arr, name);
			return null;
		}
		public virtual XVar assign_function(dynamic _param_name, dynamic _param_func, dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			dynamic func = XVar.Clone(_param_func);
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			this.xt_vars.InitAndSetArrayItem(XTempl.create_function_assignment((XVar)(func), (XVar)(var_params)), name);
			return null;
		}
		public static XVar create_function_assignment(dynamic _param_func, dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic func = XVar.Clone(_param_func);
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			return new XVar("func", func, "params", var_params);
		}
		public virtual XVar assign_method(dynamic _param_name, dynamic var_object, dynamic _param_method, dynamic _param_params = null)
		{
			#region default values
			if(_param_params as Object == null) _param_params = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			dynamic method = XVar.Clone(_param_method);
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			this.xt_vars.InitAndSetArrayItem(XTempl.create_method_assignment((XVar)(method), (XVar)(var_object), (XVar)(var_params)), name);
			return null;
		}
		public static XVar create_method_assignment(dynamic _param_method, dynamic var_object, dynamic _param_params = null)
		{
			#region default values
			if(_param_params as Object == null) _param_params = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic method = XVar.Clone(_param_method);
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			return new XVar("method", method, "params", var_params, "object", var_object);
		}
		public virtual XVar unassign(dynamic _param_name)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			#endregion

			this.xt_vars.Remove(name);
			return null;
		}
		public virtual XVar assign_event(dynamic _param_name, dynamic var_object, dynamic _param_method, dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			dynamic method = XVar.Clone(_param_method);
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			this.xt_events.InitAndSetArrayItem(new XVar("method", method, "params", var_params), name);
			this.xt_events.InitAndSetArrayItem(var_object, name, "object");
			return null;
		}
		public virtual XVar xt_doevent(dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			return null;
		}
		public virtual XVar fetchVar(dynamic _param_varName)
		{
			#region pass-by-value parameters
			dynamic varName = XVar.Clone(_param_varName);
			#endregion

			dynamic varParams = null, var_out = null;
			MVCFunctions.ob_start();
			varParams = XVar.Clone(XVar.Array());
			processVar((XVar)(getVar((XVar)(varName))), (XVar)(varParams));
			var_out = XVar.Clone(MVCFunctions.ob_get_contents());
			MVCFunctions.ob_end_clean();
			return var_out;
		}
		public virtual XVar fetch_loaded(dynamic _param_filtertag = null)
		{
			#region default values
			if(_param_filtertag as Object == null) _param_filtertag = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic filtertag = XVar.Clone(_param_filtertag);
			#endregion

			dynamic var_out = null;
			MVCFunctions.ob_start();
			display_loaded((XVar)(filtertag));
			var_out = XVar.Clone(MVCFunctions.ob_get_contents());
			MVCFunctions.ob_end_clean();
			return var_out;
		}
		public virtual XVar call_func(dynamic _param_var)
		{
			#region pass-by-value parameters
			dynamic var_var = XVar.Clone(_param_var);
			#endregion

			return null;
		}
		public virtual XVar set_template(dynamic _param_template)
		{
			#region pass-by-value parameters
			dynamic template = XVar.Clone(_param_template);
			#endregion

			dynamic templatesPath = null;
			this.template_file = XVar.Clone(MVCFunctions.basename((XVar)(template), new XVar(".htm")));
			set_layout();
			templatesPath = new XVar("templates/");
			if(XVar.Pack(mobileTemplateMode()))
			{
				templatesPath = new XVar("mobile/");
			}
			if(XVar.Pack(MVCFunctions.file_exists((XVar)(MVCFunctions.getabspath((XVar)(MVCFunctions.Concat(templatesPath, template)))))))
			{
				this.template = XVar.Clone(MVCFunctions.myfile_get_contents((XVar)(MVCFunctions.getabspath((XVar)(MVCFunctions.Concat(templatesPath, template))))));
			}
			if((XVar)(mobileTemplateMode())  && (XVar)(this.template == ""))
			{
				templatesPath = new XVar("templates/");
				this.template = XVar.Clone(MVCFunctions.myfile_get_contents((XVar)(MVCFunctions.getabspath((XVar)(MVCFunctions.Concat(templatesPath, template))))));
			}
			assign_headers();
			return null;
		}
		public virtual XVar set_layout()
		{
			this.layout = GlobalVars.page_layouts[this.template_file];
			return null;
		}
		public virtual XVar prepare_template(dynamic _param_template)
		{
			#region pass-by-value parameters
			dynamic template = XVar.Clone(_param_template);
			#endregion

			prepareContainers();
			return null;
		}
		public virtual XVar load_template(dynamic _param_template)
		{
			#region pass-by-value parameters
			dynamic template = XVar.Clone(_param_template);
			#endregion

			set_template((XVar)(template));
			prepareContainers();
			return null;
		}
		public virtual XVar display_loaded(dynamic _param_filtertag = null)
		{
			#region default values
			if(_param_filtertag as Object == null) _param_filtertag = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic filtertag = XVar.Clone(_param_filtertag);
			#endregion

			return null;
		}
		public virtual XVar display(dynamic _param_template)
		{
			#region pass-by-value parameters
			dynamic template = XVar.Clone(_param_template);
			#endregion

			return null;
		}
		public virtual XVar displayPartial(dynamic _param_template)
		{
			#region pass-by-value parameters
			dynamic template = XVar.Clone(_param_template);
			#endregion

			dynamic savedTemplate = null;
			savedTemplate = XVar.Clone(this.template);
			display((XVar)(template));
			this.template = XVar.Clone(savedTemplate);
			return null;
		}
		public virtual XVar processVar(dynamic var_var, dynamic varparams)
		{
			return null;
		}
		public virtual XVar displayBricksHidden(dynamic _param_bricks)
		{
			#region pass-by-value parameters
			dynamic bricks = XVar.Clone(_param_bricks);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> name in bricks.GetEnumerator())
			{
				this.hiddenBricks.InitAndSetArrayItem(true, name.Value);
			}
			return null;
		}
		public virtual XVar displayBrickHidden(dynamic _param_name)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			#endregion

			this.hiddenBricks.InitAndSetArrayItem(true, name);
			return null;
		}
		public virtual XVar hideAllBricksExcept(dynamic _param_arrExceptBricks)
		{
			#region pass-by-value parameters
			dynamic arrExceptBricks = XVar.Clone(_param_arrExceptBricks);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> container in this.layout.containers.GetEnumerator())
			{
				foreach (KeyValuePair<XVar, dynamic> brick in container.Value.GetEnumerator())
				{
					if(XVar.Pack(!(XVar)(MVCFunctions.in_array((XVar)(brick.Value["name"]), (XVar)(arrExceptBricks)))))
					{
						assign((XVar)(brick.Value["block"]), new XVar(false));
					}
				}
			}
			return null;
		}
		public virtual XVar showBrick(dynamic _param_name)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> container in this.layout.containers.GetEnumerator())
			{
				foreach (KeyValuePair<XVar, dynamic> brick in container.Value.GetEnumerator())
				{
					if(brick.Value["name"] == name)
					{
						assign((XVar)(brick.Value["block"]), new XVar(true));
					}
				}
			}
			return null;
		}
		private XVar setContainerDisplayed(dynamic _param_cname, dynamic _param_show, dynamic _param_firstContainerSubstyle, dynamic _param_lastContainerSubstyle)
		{
			#region pass-by-value parameters
			dynamic cname = XVar.Clone(_param_cname);
			dynamic show = XVar.Clone(_param_show);
			dynamic firstContainerSubstyle = XVar.Clone(_param_firstContainerSubstyle);
			dynamic lastContainerSubstyle = XVar.Clone(_param_lastContainerSubstyle);
			#endregion

			dynamic styleString = null;
			if(this.layout.version == Constants.BOOTSTRAP_LAYOUT)
			{
				assign((XVar)(MVCFunctions.Concat("container_", cname)), new XVar(true));
				if(XVar.Pack(!(XVar)(show)))
				{
					assign((XVar)(MVCFunctions.Concat(cname, "_chiddenattr")), new XVar("data-hidden"));
				}
				return null;
			}
			prepareContainerAttrs((XVar)(cname));
			if(XVar.Pack(show))
			{
				styleString = XVar.Clone(this.preparedContainers[cname]["showString"]);
				unassign((XVar)(MVCFunctions.Concat("wrapperclass_", cname)));
			}
			else
			{
				styleString = XVar.Clone(this.preparedContainers[cname]["hideString"]);
				assign((XVar)(MVCFunctions.Concat("wrapperclass_", cname)), new XVar("rnr-hiddencontainer"));
			}
			assign_section((XVar)(MVCFunctions.Concat("container_", cname)), (XVar)(MVCFunctions.Concat("<div ", styleString, ">")), new XVar("</div>"));
			assign((XVar)(MVCFunctions.Concat("cheaderclass_", cname)), (XVar)(firstContainerSubstyle));
			assign((XVar)(MVCFunctions.Concat("cfooterclass_", cname)), (XVar)(lastContainerSubstyle));
			return null;
		}
		private XVar getPageStyle()
		{
			if(XVar.Pack(MVCFunctions.postvalue(new XVar("pdf"))))
			{
				return this.layout.pdfStyle();
			}
			return this.layout.style;
		}
		private XVar prepareContainerAttrs(dynamic _param_cname)
		{
			#region pass-by-value parameters
			dynamic cname = XVar.Clone(_param_cname);
			#endregion

			dynamic hiddenStyleString = null, pageStyle = null, styleString = null;
			pageStyle = XVar.Clone(getPageStyle());
			if(XVar.Pack(this.preparedContainers.KeyExists(cname)))
			{
				return null;
			}
			this.preparedContainers.InitAndSetArrayItem(XVar.Array(), cname);
			hiddenStyleString = new XVar("");
			styleString = new XVar("");
			if(XVar.Pack(this.layout.skins.KeyExists(cname)))
			{
				dynamic buttonsClass = null, buttonsType = null, printClass = null, printMode = null, skin = null;
				skin = XVar.Clone(this.layout.skins[cname]);
				buttonsType = XVar.Clone(this.layout.skinsparams[skin]["button"]);
				buttonsClass = XVar.Clone((XVar.Pack(buttonsType == "button2") ? XVar.Pack(" aslinks") : XVar.Pack(" asbuttons")));
				printMode = XVar.Clone(this.layout.container_properties[cname]["print"]);
				printClass = new XVar("");
				if(printMode == "repeat")
				{
					printClass = new XVar(" rp-repeat");
				}
				else
				{
					if(printMode == "none")
					{
						printClass = new XVar(" rp-noprint");
					}
				}
				if(this.layout.version == 1)
				{
					styleString = XVar.Clone(MVCFunctions.Concat(" class=\"rnr-cw-", cname, " runner-s-", skin, " ", pageStyle));
				}
				else
				{
					styleString = XVar.Clone(MVCFunctions.Concat(" class=\"rnr-cw-", cname, " rnr-s-", skin, buttonsClass, " ", pageStyle, printClass));
				}
				hiddenStyleString = XVar.Clone(MVCFunctions.Concat(styleString, " rnr-hiddencontainer"));
				styleString = MVCFunctions.Concat(styleString, "\"");
				hiddenStyleString = MVCFunctions.Concat(hiddenStyleString, "\"");
				this.preparedContainers.InitAndSetArrayItem(new XVar("showString", styleString, "hideString", hiddenStyleString), cname);
			}
			return null;
		}
		public virtual XVar prepareContainers()
		{
			dynamic classPrefix = null, containerCss = null, containersNames = XVar.Array(), displayed_containers = XVar.Array(), hidden_containers = XVar.Array(), pageStyle = null;
			if(XVar.Pack(!(XVar)(this.layout)))
			{
				return null;
			}
			containerCss = XVar.Clone(XVar.Array());
			pageStyle = XVar.Clone(getPageStyle());
			classPrefix = new XVar("rnr-");
			if(this.layout.version == 1)
			{
				classPrefix = new XVar("runner-");
			}
			assign(new XVar("stylename"), (XVar)(MVCFunctions.Concat(pageStyle, " page-", this.layout.name)));
			assign(new XVar("pageStyleName"), (XVar)(pageStyle));
			displayed_containers = XVar.Clone(XVar.Array());
			hidden_containers = XVar.Clone(XVar.Array());
			containersNames = XVar.Clone(MVCFunctions.array_keys((XVar)(this.layout.containers)));
			containersNames = XVar.Clone(MVCFunctions.array_reverse((XVar)(containersNames)));
			foreach (KeyValuePair<XVar, dynamic> cname in containersNames.GetEnumerator())
			{
				dynamic container = XVar.Array(), firstContainerSubstyle = null, hideContainer = null, lastContainerSubstyle = null, showContainer = null;
				container = XVar.Clone(this.layout.containers[cname.Value]);
				if((XVar)(this.xt_vars.KeyExists(MVCFunctions.Concat("container_", cname.Value)))  && (XVar)(XVar.Equals(XVar.Pack(this.xt_vars[MVCFunctions.Concat("container_", cname.Value)]), XVar.Pack(false))))
				{
					continue;
				}
				firstContainerSubstyle = new XVar("");
				lastContainerSubstyle = new XVar("");
				showContainer = new XVar(false);
				hideContainer = new XVar(true);
				foreach (KeyValuePair<XVar, dynamic> brick in container.GetEnumerator())
				{
					dynamic hideBrick = null;
					if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(brick.Value["block"])))))
					{
						showContainer = new XVar(true);
					}
					else
					{
						if(XVar.Pack(!(XVar)(this.xt_vars.KeyExists(brick.Value["block"]))))
						{
							continue;
						}
						else
						{
							if(XVar.Pack(!(XVar)(this.xt_vars[brick.Value["block"]])))
							{
								continue;
							}
						}
					}
					if(XVar.Pack(!(XVar)(firstContainerSubstyle)))
					{
						firstContainerSubstyle = XVar.Clone(MVCFunctions.Concat("runner-toprow style", brick.Value["substyle"]));
						if(brick.Value["name"] == "vmenu")
						{
							firstContainerSubstyle = new XVar("runner-toprow runner-vmenu");
						}
					}
					lastContainerSubstyle = XVar.Clone(MVCFunctions.Concat("runner-bottomrow style", brick.Value["substyle"]));
					if(brick.Value["name"] == "vmenu")
					{
						lastContainerSubstyle = new XVar("runner-bottomrow runner-vmenu");
					}
					showContainer = new XVar(true);
					if((XVar)(this.hiddenBricks[brick.Value["name"]])  || (XVar)((XVar)(brick.Value["name"] == "wrapper")  && (XVar)((XVar)(hidden_containers.KeyExists(brick.Value["container"]))  || (XVar)(!(XVar)(displayed_containers.KeyExists(brick.Value["container"]))))))
					{
						hideBrick = new XVar(true);
					}
					else
					{
						hideBrick = new XVar(false);
						hideContainer = new XVar(false);
					}
					if(this.layout.version != Constants.BOOTSTRAP_LAYOUT)
					{
						if(XVar.Pack(hideBrick))
						{
							assign((XVar)(MVCFunctions.Concat("brickclass_", brick.Value["name"])), (XVar)(MVCFunctions.Concat(classPrefix, "hiddenbrick")));
						}
						else
						{
							unassign((XVar)(MVCFunctions.Concat("brickclass_", brick.Value["name"])));
						}
					}
					else
					{
						if(XVar.Pack(hideBrick))
						{
							assign((XVar)(MVCFunctions.Concat(brick.Value["name"], "_hiddenattr")), new XVar("data-hidden"));
						}
					}
				}
				if(XVar.Pack(showContainer))
				{
					if(XVar.Pack(hideContainer))
					{
						hidden_containers.InitAndSetArrayItem(true, cname.Value);
					}
					setContainerDisplayed((XVar)(cname.Value), (XVar)(!(XVar)(hideContainer)), (XVar)(firstContainerSubstyle), (XVar)(lastContainerSubstyle));
					displayed_containers.InitAndSetArrayItem(true, cname.Value);
					unassign((XVar)(MVCFunctions.Concat("wrapperclass_", cname.Value)));
				}
				else
				{
					unassign((XVar)(MVCFunctions.Concat("container_", cname.Value)));
					assign((XVar)(MVCFunctions.Concat("wrapperclass_", cname.Value)), (XVar)(MVCFunctions.Concat(classPrefix, "hiddencontainer")));
				}
			}
			foreach (KeyValuePair<XVar, dynamic> block in this.layout.blocks.GetEnumerator())
			{
				dynamic hideBlock = null, showBlock = null;
				showBlock = new XVar(false);
				hideBlock = new XVar(true);
				foreach (KeyValuePair<XVar, dynamic> cname in block.Value.GetEnumerator())
				{
					if(XVar.Pack(displayed_containers[cname.Value]))
					{
						showBlock = new XVar(true);
						if(XVar.Pack(!(XVar)(hidden_containers[cname.Value])))
						{
							hideBlock = new XVar(false);
							break;
						}
					}
				}
				if(this.layout.version != Constants.BOOTSTRAP_LAYOUT)
				{
					if((XVar)(!(XVar)(showBlock))  || (XVar)(hideBlock))
					{
						assign((XVar)(MVCFunctions.Concat("blockclass_", block.Key)), (XVar)(MVCFunctions.Concat(classPrefix, "hiddenblock")));
					}
				}
				else
				{
					if((XVar)(!(XVar)(showBlock))  || (XVar)(hideBlock))
					{
						assign((XVar)(MVCFunctions.Concat("blockattr_", block.Key)), new XVar("data-hidden"));
					}
				}
			}
			if(this.layout.version == Constants.BOOTSTRAP_LAYOUT)
			{
				assign(new XVar("pageid"), (XVar)(this.pageId));
			}
			return null;
		}
		public virtual XVar hideField(dynamic _param_fieldName)
		{
			#region pass-by-value parameters
			dynamic fieldName = XVar.Clone(_param_fieldName);
			#endregion

			if(this.layout.version == 1)
			{
				assign((XVar)(MVCFunctions.Concat("fielddispclass_", MVCFunctions.GoodFieldName((XVar)(fieldName)))), new XVar("runner-hiddenfield"));
			}
			else
			{
				assign((XVar)(MVCFunctions.Concat("fielddispclass_", MVCFunctions.GoodFieldName((XVar)(fieldName)))), new XVar("rnr-hiddenfield"));
			}
			return null;
		}
		public virtual XVar showField(dynamic _param_fieldName)
		{
			#region pass-by-value parameters
			dynamic fieldName = XVar.Clone(_param_fieldName);
			#endregion

			assign((XVar)(MVCFunctions.Concat("fielddispclass_", MVCFunctions.GoodFieldName((XVar)(fieldName)))), new XVar(""));
			return null;
		}
		public virtual XVar xt_displaymenu(dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			dynamic menuparams = XVar.Array();
			menuparams = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> p in var_params.GetEnumerator())
			{
				menuparams.InitAndSetArrayItem(p.Value, null);
			}
			if(XVar.Pack(!(XVar)(GlobalVars.pageObject as object != null)))
			{
				return null;
			}
			GlobalVars.pageObject.displayMenu((XVar)(menuparams[0]), (XVar)(menuparams[1]));
			return null;
		}
		public virtual XVar xt_displaymainmenu(dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			MVCFunctions.array_unshift((XVar)(var_params), new XVar("main"));
			return xt_displaymenu((XVar)(var_params));
		}
		public virtual XVar mobileTemplateMode()
		{
			if(XVar.Pack(this.layout))
			{
				return (XVar)(CommonFunctions.mobileDeviceDetected())  && (XVar)(this.layout.version != Constants.BOOTSTRAP_LAYOUT);
			}
			else
			{
				return false;
			}
			return null;
		}
	}
}
