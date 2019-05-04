using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Linq;
using Tms.Web.Interfaces;

namespace Tms.Web.Helpers
{
	[HtmlTargetElement(Attributes = AutoSaveAttributeName)]
	public class AutoSaveTagHelper : TagHelper
	{
		private const string AutoSaveAttributeName = "auto-save";
		private IScriptHelper _scriptHelper { get; }
		private IHttpContextAccessor _httpContextAccessor;

		[HtmlAttributeName(AutoSaveAttributeName)]
		public bool AutoSave { get; set; } = false;

		public AutoSaveTagHelper(IScriptHelper scriptHelper, IHttpContextAccessor httpContextAccessor)
		{
			_scriptHelper = scriptHelper;
			_httpContextAccessor = httpContextAccessor;
		}

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			if (AutoSave)
			{
				var colName = GetAttributeValue(context, ColumnNameTagHelper.ColumnNameAttributeName);
				if (String.IsNullOrEmpty(colName))
				{
					var forAttribute = context.AllAttributes.SingleOrDefault(x => x.Name.Equals("asp-for"));
					colName = ((ModelExpression)forAttribute.Value).Name;
				}

				var id = string.Empty;
				var idAttribute = output.Attributes.SingleOrDefault(x => x.Name.Equals("id"));
				if (idAttribute != null)
				{
					id = idAttribute.Value.ToString();
				}
				else
				{
					id = colName + "-auto-save-ctrl";
					output.Attributes.Add("id", id);
				}

				var apiPath = "api/Training/SaveData?";
				var scriptText = $@"
				$('#{id}').on('change', function () {{
					debugger
					var closestForm = $(this).closest('form');
					var tblNameAttrVal = closestForm.data('tableName');
					var tblIdAttrVal = closestForm.data('tableId');
					if(tblNameAttrVal == null || tblNameAttrVal === ''){{
						tblNameAttrVal = $(this).data('tableName');
					}};
					if(tblIdAttrVal == null || tblIdAttrVal === ''){{
						tblIdAttrVal = $(this).data('tableId');
					}};
					$.ajax({{
						type: 'POST',
						url: '{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/{apiPath}'+'tableName='+tblNameAttrVal+'&columnName={colName}&colValue='+$(this).val()+'&id='+tblIdAttrVal,
						dataType: 'json',						
					}})
				}});
				";
				_scriptHelper.AddScriptText(scriptText);
			}
		}

		public static string GetAttributeValue(TagHelperContext context, string attibuteName)
		{
			var val = string.Empty;
			var attribute = context.AllAttributes.SingleOrDefault(x => x.Name.Equals(attibuteName));
			if (attribute != null)
				val = attribute.Value.ToString();
			return val;
		}
	}

	[HtmlTargetElement(Attributes = ColumnNameAttributeName)]
	public class ColumnNameTagHelper : TagHelper
	{
		public const string ColumnNameAttributeName = "column-name";

		/// <summary>
		/// The id of the modal that will be toggled by this element
		/// </summary>
		[HtmlAttributeName(ColumnNameAttributeName)]
		public string ColumnName { get; set; }
	}

	[HtmlTargetElement(Attributes = TableNameAttributeName)]
	public class TableNameTagHelper : TagHelper
	{
		public const string TableNameAttributeName = "table-name";
		private IScriptHelper _scriptHelper { get; }

		public TableNameTagHelper(IScriptHelper scriptHelper)
		{
			_scriptHelper = scriptHelper;
		}

		/// <summary>
		/// The id of the modal that will be toggled by this element
		/// </summary>
		[HtmlAttributeName(TableNameAttributeName)]
		public string TableName { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			var tblName = AutoSaveTagHelper.GetAttributeValue(context, TableNameTagHelper.TableNameAttributeName);

			var tblId = AutoSaveTagHelper.GetAttributeValue(context, TableIdentifierTagHelper.TableIdentifierAttributeName);

			var id = string.Empty;
			var idAttribute = output.Attributes.SingleOrDefault(x => x.Name.Equals("id"));
			if (idAttribute != null)
			{
				id = idAttribute.Value.ToString();
			}
			else
			{
				id = tblName + tblId + "-auto-save-block";
				output.Attributes.Add("id", id);
			}

			var scriptText = $@"
				$('#{id}').data('tableName','{tblName}');
				$('#{id}').data('tableId','{tblId}');
				";
			_scriptHelper.AddScriptText(scriptText);
		}
	}

	[HtmlTargetElement(Attributes = TableIdentifierAttributeName)]
	public class TableIdentifierTagHelper : TagHelper
	{
		public const string TableIdentifierAttributeName = "table-identifier";

		/// <summary>
		/// The id of the modal that will be toggled by this element
		/// </summary>
		[HtmlAttributeName(TableIdentifierAttributeName)]
		public string TableIdentifier { get; set; }
	}
}