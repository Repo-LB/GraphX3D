'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("DemoProject.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to # Blender v2.92.0 OBJ File: &apos;&apos;
        '''# www.blender.org
        '''o Cone
        '''v 0.000000 1.000000 -1.000000
        '''v -0.195090 1.000000 -0.980785
        '''v -0.382683 1.000000 -0.923880
        '''v -0.555570 1.000000 -0.831470
        '''v -0.707107 1.000000 -0.707107
        '''v -0.831470 1.000000 -0.555570
        '''v -0.923879 1.000000 -0.382683
        '''v -0.980785 1.000000 -0.195090
        '''v -1.000000 1.000000 -0.000000
        '''v -0.980785 1.000000 0.195090
        '''v -0.923880 1.000000 0.382683
        '''v -0.831470 1.000000 0.555570
        '''v -0.707107 1.000000 0.707107
        '''v -0.555570 1.000000 0.831470
        '''v -0.382683 1.000000 0.92388 [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property Cone() As String
            Get
                Return ResourceManager.GetString("Cone", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
