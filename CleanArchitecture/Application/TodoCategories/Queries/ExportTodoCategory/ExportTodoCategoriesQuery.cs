using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoCategories.Queries.ExportTodoCategory
{
    public class ExportTodoCategoriesQuery : IRequest<ExportTodoCategoriesVm>
    {
        public int CategoryId { get; set; }

    }

    public class ExportTodoCategoriesQueryHandler : IRequestHandler<ExportTodoCategoriesQuery, ExportTodoCategoriesVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICsvFileBuilder _fileBuilder;

        public ExportTodoCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper, ICsvFileBuilder fileBuilder)
        {
            _context = context;
            _mapper = mapper;
            _fileBuilder = fileBuilder;
        }

        public async Task<ExportTodoCategoriesVm> Handle(ExportTodoCategoriesQuery request, CancellationToken cancellationToken)
        {
            var vm = new ExportTodoCategoriesVm();
            var records = await _context.TodoItems
                .Where(x => x.CategoryId == request.CategoryId)
                .ProjectTo<TodoCategoryItemFileRecord>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            vm.Content = _fileBuilder.BuildTodoItemsFile(records);
            vm.ContentType = "text/csv";
            vm.FileName = "TodoItems.csv";

            return await Task.FromResult(vm);
        }
    }
}
