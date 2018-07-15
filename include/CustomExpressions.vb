Imports System
Imports System.Diagnostics
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Collections
Imports System.Dynamic
Imports System.ComponentModel.Composition
Imports runnerDotNet
Imports System.ComponentModel.Composition.Hosting
Imports System.IO
Imports System.Reflection

Namespace runnerDotNet
	<Export(GetType(ICustomExpressionProviderVB))>
	Public Class CustomExpressionProvider : Implements ICustomExpressionProviderVB
		Public Function GetCustomExpression(value As XVar, data As XVar, field As XVar, ptype As XVar, Optional table As XVar = Nothing) As XVar Implements ICustomExpressionProviderVB.GetCustomExpression
			Return value
		End Function

		Public Function GetFileCustomExpression(file As XVar, data As XVar, field As XVar, ptype As XVar, Optional table As XVar = Nothing) As XVar Implements ICustomExpressionProviderVB.GetFileCustomExpression
			Dim value as New XVar()
			value = CType("", XVar)
			Return value
		End Function

		Public Function GetLWWhere(field As XVar, ptype As XVar, Optional table As XVar = Nothing) As XVar Implements ICustomExpressionProviderVB.GetLWWhere
			Return ""
		End Function

		Public Function GetDefaultValue(field As XVar, ptype As XVar, Optional table As XVar = Nothing) As XVar Implements ICustomExpressionProviderVB.GetDefaultValue
			Return ""
		End Function

		Public Function GetAutoUpdateValue(field As XVar, ptype As XVar, Optional table As XVar = Nothing) As XVar Implements ICustomExpressionProviderVB.GetAutoUpdateValue
			Return ""
		End Function

		Public Function getCustomMapIcon(field As XVar, table As XVar, data As XVar) As XVar Implements ICustomExpressionProviderVB.getCustomMapIcon
			Return ""
		End Function

		Public Function getDashMapCustomIcon(dashName As XVar, dashElementName As XVar, data As XVar) As XVar Implements ICustomExpressionProviderVB.getDashMapCustomIcon

			Return ""
		End Function

		Public Function GetUploadFolderExpression(field As XVar, file As XVar, Optional table As XVar = Nothing) As XVar Implements ICustomExpressionProviderVB.GetUploadFolderExpression
			Return ""
		End Function

		Public Function GetIntervalLimitsExprs(table As XVar, field As XVar, idx As XVar, isLowerBound As XVar) As XVar Implements ICustomExpressionProviderVB.GetIntervalLimitsExprs
			Return ""
		End Function


	End Class
End Namespace