using Application.Common.Exceptions;
using Application.TodoItems.Queries.GetTodoItemsWithPagination;
using Application.TodoLists.Commands.CreateTodoList;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.TodoCategories.Commands.CreateTodoCategory;

namespace Application.IntegrationTests.TodoItems.Queries
{
    using static Testing;
    public class GetTodoItemTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumCategoryId()
        {
            
            var query = new GetTodoItemsWithPaginationQuery();

            FluentActions.Invoking(() =>
                SendAsync(query)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldGetTodoItemsWithPaginationQuery()
        {
            string[] item =new string[5] { "Math", "Fisika", "Kimia", "Biologi", "Sejarah" };

            var listId = await SendAsync(new CreateTodoCategoryCommand
            {
                CategoryTitle = "Belajar"
            });

            foreach (var n in item)
            {
                await SendAsync(new CreateTodoItemCommand
                {
                    CategoryId = listId,
                    ActivityTitle = "Belajar " + n
                });
            }

            var query = await SendAsync(new GetTodoItemsWithPaginationQuery
            {
                CategoryId = listId,
                PageNumber = 1,
                PageSize = 2,
            });

/*            query.HasNextPage.Should().BeTrue();
            query.HasPreviousPage.Should().BeFalse();
            query.TotalPages.Should().Be(3);
            query.PageIndex.Should().Be(1);
            query.TotalCount.Should().Be(5);*/

        }
    }
}
