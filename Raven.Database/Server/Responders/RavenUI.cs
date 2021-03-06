//-----------------------------------------------------------------------
// <copyright file="RavenUI.cs" company="Hibernating Rhinos LTD">
//     Copyright (c) Hibernating Rhinos LTD. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.IO;
using System.Linq;
using Raven.Database.Extensions;
using Raven.Database.Server.Abstractions;

namespace Raven.Database.Server.Responders
{
	public class RavenUI : AbstractRequestResponder
	{
		public override string UrlPattern
		{
			get { return "^/raven/"; }
		}

		public override bool IsUserInterfaceRequest
		{
			get { return true; }
		}

		public override string[] SupportedVerbs
		{
			get { return new[] {"GET"}; }
		}

		public override void Respond(IHttpContext context)
		{
			if (string.IsNullOrEmpty(Database.Configuration.RedirectStudioUrl) == false)
			{
				context.Response.Redirect(Database.Configuration.RedirectStudioUrl);
				return;
			}

			var docPath = context.GetRequestUrl().Replace("/raven/", "");
			context.WriteEmbeddedFile(Settings.WebDir, docPath);
		}
	}
}