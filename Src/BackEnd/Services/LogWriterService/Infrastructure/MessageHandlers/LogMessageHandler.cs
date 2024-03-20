﻿using LogWriterService.Application.Repositories;
using LogWriterService.Core.DbEntities;

namespace LogWriterService.Infrastructure.MessageHandlers;

public sealed class LogMessageHandler : MessageHandlerBase<LogMessage>
{
    private readonly ILogger<LogMessageHandler> _logger;
    private readonly ILogRepository _logRepository;

    public LogMessageHandler(ILogger<LogMessageHandler> logger, ILogRepository logRepository)
    {
        _logger = logger;
        _logRepository = logRepository;
    }

    public override async Task Handle(LogMessage message)
    {
        var logDbEntity = new LogDbEntity
        {
            AppName = message.AppName,
            Log = message.Level,
            Message = message.Message,
            Method = message.Method,
            DateTime = message.DateTime
        };
        await _logRepository.Save(logDbEntity);
    }
}