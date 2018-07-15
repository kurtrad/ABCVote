Option Strict Off
Imports System
Imports System.Diagnostics
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Collections
Imports System.Dynamic
Imports System.ComponentModel.Composition
Imports System.Web
Imports runnerDotNet

Namespace runnerDotNet
	<Export(GetType(IGlobalEventProviderVB))>
	Public Class CommonEvents : Implements IGlobalEventProviderVB

		' Captchas functions

		' handlers

		' onscreen events

		' global captchas, buttons, ajax

		' table maps, buttons, ajax




		Public Function AfterTableInit(context As Object) As XVar Implements IGlobalEventProviderVB.AfterTableInit
			Dim table = context("table")
			Dim query = context("query")
			context("query") = query
			Return Nothing
		End Function

		Public Function GetTablePermissions(permissions As Object, Optional table As Object = Nothing) As XVar Implements IGlobalEventProviderVB.GetTablePermissions
			If table Is Nothing OrElse table = CType("", XVar) Then
				table = GlobalVars.strTableName
			End If
			Return permissions
		End Function

		Public Function IsRecordEditable(values As Object, isEditable As Object, Optional table As Object = Nothing) As XVar Implements IGlobalEventProviderVB.IsRecordEditable
			If table Is Nothing OrElse table = CType("", XVar) Then
				table = GlobalVars.strTableName
			End If
			Return isEditable
		End Function
	End Class
End Namespace