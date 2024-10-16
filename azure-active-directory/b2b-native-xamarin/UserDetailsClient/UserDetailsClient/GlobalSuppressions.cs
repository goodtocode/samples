﻿
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("AsyncUsage.CSharp.Reliability", "AvoidAsyncVoid:Avoid async void", Justification = "(false positive): event handlers are the only async method that can return void", Scope = "member", Target = "~M:MyCo.Identity.MainPage.OnSignInSignOut(System.Object,System.EventArgs)")]