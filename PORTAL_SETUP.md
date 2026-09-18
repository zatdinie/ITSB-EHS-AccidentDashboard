# ITSB Accident Dashboard in EHS Portal — setup

This branch (`EHS_PORTAL`) is the EHS Portal *area* version of the ITSB Accident
Dashboard. It lives in the portal as the git submodule `Areas/ACCIDENT`:

- the MVC part (Controllers, Models, Views, Content, Scripts,
  `ACCIDENTAreaRegistration.cs`) is compiled into `EHS_PORTAL.dll`
- `ITSB.AccidentDashboard.Core/` stays its own project
  (`ITSB.AccidentDashboard.Core.csproj`, referenced by `EHS_PORTAL.csproj` and
  listed in `EHS_PORTAL.sln`) and builds to `ITSB.AccidentDashboard.Core.dll`

The standalone app is on branch `main`; this branch does **not** build on its
own (its NuGet paths point at the portal's `packages` folder).

## 1. Get the code

```bash
git clone --recurse-submodules https://github.com/afiqqo231/EHS_PORTAL.git
# or, in an existing clone:
git submodule update --init Areas/ACCIDENT
```

To pull a newer version of this area later:

```bash
git submodule update --remote Areas/ACCIDENT
git add Areas/ACCIDENT && git commit -m "update ACCIDENT submodule"
```

## 2. Build

Open `EHS_PORTAL.sln` in Visual Studio (two projects: `EHS_PORTAL` and
`ITSB.AccidentDashboard.Core`) → Rebuild Solution. If Core complains about a
missing `EntityFramework.6.5.1` package: right-click the solution →
*Restore NuGet Packages*.

Targets .NET Framework 4.7.2 / EF 6.5.1 / MVC 5 — the same as the portal.

## 3. Database (run once per database)

Data lives in the portal database (`DefaultConnection`, ESH) under its own
schema **`ACCIDENT`** (`ACCIDENT.Plants`, `ACCIDENT.AccidentIncidents`,
`ACCIDENT.PlantGroups`, `ACCIDENT.BodyParts`, `ACCIDENT.InjuryTypes`,
`ACCIDENT.AccidentCauses`, `ACCIDENT.MonthlyManHours`). Nothing in `dbo` or
`FETS` is touched.

In Visual Studio → Tools → NuGet Package Manager → Package Manager Console:

```powershell
Update-Database -ProjectName ITSB.AccidentDashboard.Core -StartUpProjectName EHS_PORTAL
```

- `-StartUpProjectName EHS_PORTAL` makes EF read `DefaultConnection` from the
  portal's Web.config.
- Applies the migrations in `ITSB.AccidentDashboard.Core/Migrations` and runs
  `Configuration.Seed()` (plants, plant groups, body parts, injury types,
  accident causes). Safe to re-run.
- To review the SQL first: add `-Script`.

`Database.SetInitializer<EshDbContext>(null)` is set in the area registration,
so EF never creates or alters tables by itself — always use `Update-Database`.

When the entities change: `Add-Migration <Name> -ProjectName ITSB.AccidentDashboard.Core -StartUpProjectName EHS_PORTAL`,
commit the generated files, then `Update-Database` on every environment.

## 4. Web.config

No settings are required. The area uses the portal's existing `DefaultConnection`.

## URLs

| What | URL |
|---|---|
| Dashboard (default) | `/ACCIDENT/` or `/ACCIDENT/Dashboard` |
| Pages | `/ACCIDENT/StatusByPlant`, `/ACCIDENT/AccidentAnalysis`, `/ACCIDENT/LWR`, `/ACCIDENT/AccidentIncident`, `/ACCIDENT/MasterData` (Plant, BodyPart, InjuryType, AccidentCause, MonthlyManHours) |
| Area assets | `/Areas/ACCIDENT/Content/...`, `/Areas/ACCIDENT/Scripts/...` |
| Bundles | `~/bundles/accident/jquery`, `~/bundles/accident/bootstrap`, `~/bundles/accident/modernizr`, `~/Content/accident/css` (registered in `ACCIDENTAreaRegistration`; the portal's own bundles are untouched) |

Charts load Chart.js 3.9.1 from cdnjs (needs internet access from the browser).

## Access control

**None yet.** All `/ACCIDENT/...` pages, including Master Data and Accident
Incident editing, are reachable without login (kept open for testing). Add
`[Authorize]` / a portal login requirement before production use.

## Portal changes this area depends on (already in portal git)

- `EHS_PORTAL.csproj`: `Compile Include="Areas\ACCIDENT\**\*.cs"` excluding
  `Areas\ACCIDENT\ITSB.AccidentDashboard.Core\**`; `Content` includes for
  `Views`, `Views\Web.config`, `Content`, `Scripts`; `ProjectReference` to
  `Areas\ACCIDENT\ITSB.AccidentDashboard.Core\ITSB.AccidentDashboard.Core.csproj`.
- `EHS_PORTAL.sln`: project `ITSB.AccidentDashboard.Core`
  (`{9662FD22-C37D-4C7B-BE72-CAA147CA4554}`).
- Homepage card: `index.html` ("Accident Dashboard", `images/data.png`).

## Notes

- Namespaces are unchanged from the standalone app
  (`ITSB.AccidentDashboard.Web.*`, `ITSB.AccidentDashboard.Core.*`); the area
  route is restricted to `ITSB.AccidentDashboard.Web.Controllers`, so its
  `DashboardController` does not clash with CORD's.
- `Core` is a separate assembly on purpose: its `Plant` entity would otherwise
  clash with the `Plant` classes of CLIP, ESTAFF and FETS_MOBILE.
- Views/Web.config targets MVC 5.3.0.0 (portal version).
