using Microsoft.EntityFrameworkCore;
using MyCV_labb3.DBContext;
using MyCV_labb3.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CV_DBContext>();
builder.Services.AddAuthorization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Read all users
app.MapGet("/Users", async (CV_DBContext dbContext) =>
{
    try
    {
        var users = await dbContext.Users.Include(u => u.Skills).Include(u => u.Projects).ToListAsync();
        return Results.Ok(users);
    }
    catch
    {
        return Results.Problem("Server Error");
    }
});

//Read User by UserName
app.MapGet("/users/{username}", async (CV_DBContext dbContext, string userName) =>
{
    try
    {
        var user = await dbContext.Users.Include(u => u.Skills).Include(u => u.Projects)
                                         .FirstOrDefaultAsync(x => x.Username == userName);
        if (user != null)
        {
            return Results.Ok(user);
        }
        else
        {
            return Results.NotFound("No user with this ID was found");
        }
    }
    catch
    {
        return Results.Problem("Server Error");
    }
});

//Create User
app.MapPost("/users", async (CV_DBContext dbContext, UserModel user) =>
{
    try
    {
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
        return Results.Ok(user);
    }
    catch
    {
        return Results.Problem("Server Error");
    }
});


//Update user

app.MapPut("/users", async (CV_DBContext dbContext, UserModel user) =>
{
    try
    {
        var userToUpdate = await dbContext.Users.Include(u => u.Skills).Include(u => u.Projects).FirstOrDefaultAsync(x => x.Id == user.Id);

        if (userToUpdate != null)
        {
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Username = user.Username;           
            userToUpdate.Email = user.Email;          

            if (user.Skills != null)
            {
                foreach (var skill in user.Skills)
                {
                    if(userToUpdate.Skills.Any(s=>s.SkillId == skill.SkillId))
                    {
                        userToUpdate.Skills.Remove(skill);
                    }
                    else
                    {
                        userToUpdate.Skills.Add(skill);
                    }                  
                }
            }          
            if (user.Projects != null)
            {
                foreach (var project in user.Projects)
                {
                    if(userToUpdate.Projects.Any(p => p.ProjectId== project.ProjectId))
                    {
                        userToUpdate.Projects.Remove(project);
                    }
                    else
                    {
                        userToUpdate.Projects.Add(project);
                    }
                    //var projectToUpdate = userToUpdate.Projects.FirstOrDefault(p => p.ProjectId == project.ProjectId);
                    //if (projectToUpdate != null)
                    //{
                    //    projectToUpdate.ProjectName = project.ProjectName;
                    //    projectToUpdate.ProjectDescription = project.ProjectDescription;
                    //    userToUpdate.Projects.Add(projectToUpdate);
                    //}
                    //else
                    //{
                    //    var newProjectToAdd = new Project();
                    //    newProjectToAdd.ProjectName = project.ProjectName;
                    //    newProjectToAdd.ProjectDescription = project.ProjectDescription;
                    //    userToUpdate.Projects.Add(newProjectToAdd);
                    //}
                }
            }
            await dbContext.SaveChangesAsync();
            return Results.Ok(userToUpdate);
        }
        else
        {
            return Results.NotFound("No user was found with this ID");
        }
    }
    catch
    {
        return Results.Problem("Server Error");
    }
});

/***********************************  Skill ********************************/

//Add skill
app.MapPost("/addskill/", async (CV_DBContext dbContext, Skill skill) =>
{
    try
    {        
        dbContext.Skills.Add(skill);
        await dbContext.SaveChangesAsync();
        return Results.Ok(skill);
    }
    catch
    {
        return Results.Problem("Server error"); ;
    }
});


//Update skill
app.MapPut("/updateskill/", async (CV_DBContext dbContext, Skill skill) =>
{
    try
    {
        var skillToUpdate = await dbContext.Skills.FirstOrDefaultAsync(item => item.SkillId == skill.SkillId);
        skillToUpdate.SkillName = skill.SkillName;
        skillToUpdate.SkillLevel = skill.SkillLevel;
        skillToUpdate.YearsOfExperience = skill.YearsOfExperience;       
        await dbContext.SaveChangesAsync();
        return Results.Ok(skillToUpdate);
    }
    catch
    {
        return Results.Problem("Server error"); ;
    }
});

//Delete skill
app.MapDelete("/skills/{skillId}", async (CV_DBContext dbContext, int skillId) =>
{
    try
    {
        var skillToDelete = await dbContext.Skills.FirstOrDefaultAsync(item => item.SkillId == skillId);
        dbContext.Skills.Remove(skillToDelete);
        await dbContext.SaveChangesAsync();
        return Results.Ok(skillToDelete);
    }
    catch
    {
        return Results.Problem("Server error"); ;
    }
});

/***********************************  Project ********************************/

//Add project
app.MapPost("/addproject/", async (CV_DBContext dbContext, Project project) =>
{
    try
    {
        dbContext.Projects.Add(project);
        await dbContext.SaveChangesAsync();
        return Results.Ok(project);
    }
    catch
    {
        return Results.Problem("Server error"); ;
    }
});

// Update Project
app.MapPut("/updateproject/", async (CV_DBContext dbContext, Project project) =>
{
    try
    {
        var projectToUpdate = await dbContext.Projects.FirstOrDefaultAsync(item => item.ProjectId == project.ProjectId);
        projectToUpdate.ProjectName = project.ProjectName;
        projectToUpdate.ProjectDescription = project.ProjectDescription;
        await dbContext.SaveChangesAsync();
        return Results.Ok(projectToUpdate);
    }
    catch
    {
        return Results.Problem("Server error"); ;
    }
});

//Delete project
app.MapDelete("/projects/{projectId}", async (CV_DBContext dbContext, int projectId) =>
{
    try
    {
        var projectToDelete = await dbContext.Projects.FirstOrDefaultAsync(item => item.ProjectId == projectId);
        dbContext.Projects.Remove(projectToDelete);
        await dbContext.SaveChangesAsync();
        return Results.Ok(projectToDelete);
    }
    catch
    {
        return Results.Problem("Server error"); ;
    }
});

app.Run();

