$solutionName = "HaiphongTech/src/HaiphongTech"  # Replace with your actual solution name
$projectPath = "$solutionName.API/$($solutionName.Split('/')[-1]).API.csproj"
dotnet watch run --project $projectPath