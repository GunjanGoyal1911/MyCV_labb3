using System.Linq.Expressions;
using System.Runtime.CompilerServices;
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

//Read User by ID
app.MapGet("/users/{id}", async (CV_DBContext dbContext, int id) =>
{
    try
    {
        var user = await dbContext.Users.Include(u => u.Skills).Include(u => u.Projects)
                                         .FirstOrDefaultAsync(x => x.Id == id);
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
            // Update user properties here
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Username = user.Username;
            userToUpdate.Password = user.Password;
            userToUpdate.Email = user.Email;
            userToUpdate.Role = user.Role;

            //Update skills

            if (user.Skills != null)
            {
                foreach (var skill in user.Skills)
                {
                    var skillToUpdate = userToUpdate.Skills.FirstOrDefault(s => s.SkillId == skill.SkillId);
                    if (skillToUpdate != null)
                    {
                        skillToUpdate.YearsOfExperience = skill.YearsOfExperience;
                        skillToUpdate.SkillName = skill.SkillName;
                        skillToUpdate.SkillLevel = skill.SkillLevel;
                        userToUpdate.Skills.Add(skillToUpdate);
                    }
                    else
                    {
                        var newSkilltoAdd = new Skill();
                        newSkilltoAdd.SkillName = skill.SkillName;
                        newSkilltoAdd.SkillLevel = skill.SkillLevel;
                        newSkilltoAdd.YearsOfExperience = skill.YearsOfExperience;
                        userToUpdate.Skills.Add(newSkilltoAdd);
                    }
                }
            }

            //Update project
            if (user.Projects != null)
            {
                foreach (var project in user.Projects)
                {
                    var projectToUpdate = userToUpdate.Projects.FirstOrDefault(p => p.ProjectId == project.ProjectId);
                    if (projectToUpdate != null)
                    {
                        projectToUpdate.ProjectName = project.ProjectName;
                        projectToUpdate.ProjectDescription = project.ProjectDescription;
                        userToUpdate.Projects.Add(projectToUpdate);
                    }
                    else
                    {
                        var newProjectToAdd = new Project();
                        newProjectToAdd.ProjectName = project.ProjectName;
                        newProjectToAdd.ProjectDescription = project.ProjectDescription;
                        userToUpdate.Projects.Add(newProjectToAdd);
                    }
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



//Delete user
app.MapDelete("/users/{id}", async (CV_DBContext dbContext, int id) =>
{
    try
    {
        var userToDelete = await dbContext.Users.Include(s=>s.Skills).Include(p => p.Projects).FirstOrDefaultAsync(u => u.Id == id);
        if(userToDelete != null)
        {
            // Remove associated skills
            foreach (var skill in userToDelete.Skills)
            {
                dbContext.Skills.Remove(skill);
            }

            // Remove associated projects
            foreach (var project in userToDelete.Projects)
            {
                dbContext.Projects.Remove(project);
            }
            dbContext.Users.Remove(userToDelete);
            await dbContext.SaveChangesAsync();
            return Results.Ok(userToDelete);
        }
        else
        {
            return Results.NotFound("No user found with this ID");
        }
    }
    catch
    {

        return Results.Problem("Server error"); ;
    }

});



app.Run();

