using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.GithubUpdateChecker;

public interface IVersionGetter
{
    Task<Version?> GetLatestVersion();
    string GetReleaseUrl(Version newVersion);
}
