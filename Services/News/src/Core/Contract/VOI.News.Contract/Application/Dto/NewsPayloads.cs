﻿namespace VOI.News.Contract.Application.Dto;

public sealed record NewsCreatePayload(long Id);
public sealed record NewsEditPayload(long Id);
public sealed record NewsDeletePayload(long Id);